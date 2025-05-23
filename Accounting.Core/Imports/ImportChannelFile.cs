using Accounting.Books;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accounting.Imports;

public class ImportChannelFileNotificationHandler : INotificationHandler<ImportChannelFileNotification>
{
    protected virtual ChannelFileParseService Service { get; set; }
    protected virtual ILedgerStore Store { get; set; }
    protected virtual IMediator Mediator { get; set; }
    protected virtual ILogger Logger { get; set; }

    public ImportChannelFileNotificationHandler(ChannelFileParseService service, ILedgerStore store, IMediator mediator, ILogger<ImportChannelFileNotificationHandler> logger)
    {
        this.Logger = logger;
        this.Service = service;
        this.Store = store;
        this.Mediator = mediator;
    }

    public async Task Handle(ImportChannelFileNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Record is null)
        {
            throw new ArgumentNullException("notification.Record");
        }

        try
        {
            notification.Record.Status = ImportStatus.None;
            await Store.CreateAsync(notification.Record, cancellationToken);

            await this.Mediator.Publish(new UpdateImportRecordStatusNotification { ImportRecordId = notification.Record.Id, From = ImportStatus.None, To = ImportStatus.Resolving }).ConfigureAwait(false);

            var result = await Service.ParseAsync(notification.Record, cancellationToken);

            result.ThrowIfFailed();

            if (result.Data.IsNullOrEmpty() == false)
            {
                await Store.BulkSaveChannelRecordsAsync(result.Data, cancellationToken);
            }

            await this.Mediator.Publish(new UpdateImportRecordStatusNotification { ImportRecordId = notification.Record.Id, From = ImportStatus.Resolving, To = ImportStatus.Succeeded }).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            this.Logger.LogError(ex, "导入文件失败");
            await this.Mediator.Publish(new UpdateImportRecordStatusNotification { ImportRecordId = notification.Record.Id, From = ImportStatus.Resolving, To = ImportStatus.Failed }).ConfigureAwait(false);
        }
    }
}