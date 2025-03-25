using MediatR;

namespace Accounting.Imports;

public class ImportChannelFileNotificationHandler : INotificationHandler<ImportChannelFileNotification>
{
    protected virtual ChannelFileParseService Service { get; set; }
    protected virtual IImportRecordStore Store { get; set; }

    public ImportChannelFileNotificationHandler(ChannelFileParseService service, IImportRecordStore store)
    {
        this.Service = service;
        this.Store = store;
    }

    public async Task Handle(ImportChannelFileNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Record is null)
        {
            throw new ArgumentNullException("notification.Record");
        }

        var result = await Service.ParseAsync(notification.Record, cancellationToken);

        result.ThrowIfFailed();

        if (notification.Record.Items.IsNullOrEmpty())
        {
            return;
        }

        await Store.SaveItemsAsync(notification.Record.Items.ToList(), cancellationToken);
    }
}