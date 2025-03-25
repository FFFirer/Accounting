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
}