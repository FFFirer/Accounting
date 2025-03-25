using Accounting.Common;
using Accounting.Imports;

namespace Accounting.Imports;

public class ImportRecordStore : AccountingBaseStore, IImportRecordStore
{
    public ImportRecordStore(AccountingDbContext context) : base(context)
    {
    }

    public async Task SaveAsync(ImportRecord record, CancellationToken cancellationToken)
    {
        if(record.Id > 0) {
            await this.Context.AddAsync(record, cancellationToken);
        }
        else{
            this.Context.Update(record);
        }

        await SaveChanges(cancellationToken);
    }

    public async Task SaveItemsAsync(List<ImportRecordItem> items, CancellationToken cancellationToken)
    {
        var createItems = new List<ImportRecordItem>();
        var updateItems = new List<ImportRecordItem>();

        if(createItems.IsNullOrEmpty() == false) {
            await this.Context.AddRangeAsync(createItems);
        }
        
        if(updateItems.IsNullOrEmpty() == false) {
            this.Context.UpdateRange(updateItems);
        }

        await SaveChanges(cancellationToken);
    }
}