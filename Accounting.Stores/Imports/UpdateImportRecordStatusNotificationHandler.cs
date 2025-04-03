using System;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Imports;

public class UpdateImportRecordStatusNotificationHandler : INotificationHandler<UpdateImportRecordStatusNotification>
{
    protected virtual AccountingDbContext DbContext { get; set; }

    public UpdateImportRecordStatusNotificationHandler(AccountingDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task Handle(UpdateImportRecordStatusNotification notification, CancellationToken cancellationToken)
    {
        await this.DbContext.ImportRecords
            .Where(x => x.Id == notification.ImportRecordId && x.Status == notification.From)
            .ExecuteUpdateAsync(spc => spc.SetProperty(x => x.Status, notification.To), cancellationToken);

        await this.DbContext.SaveChangesAsync(cancellationToken);
    }
}
