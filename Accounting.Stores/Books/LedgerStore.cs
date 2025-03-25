
using EFCore.BulkExtensions;

namespace Accounting.Books;

public class LedgerStore : AccountingBaseStore, ILedgerStore
{
    public LedgerStore(AccountingDbContext context) : base(context)
    {

    }

    public async Task BulkSaveChannelRecordsAsync(List<LedgerRecord> data, CancellationToken cancellationToken)
    {
        using (var trans = await this.Context.Database.BeginTransactionAsync())
        {
            await this.Context.BulkInsertOrUpdateAsync(
                data,
                bulkAction: config =>
                {
                    config.EnableShadowProperties = true;
                    config.PropertiesToExcludeOnUpdate = [nameof(LedgerRecord.LedgerId), nameof(LedgerRecord.CreatedTime), nameof(LedgerRecord.Tags), "AssetAccountId", nameof(LedgerRecord.SourceChannelCode), nameof(LedgerRecord.SourceChannelId)];
                },
                cancellationToken: cancellationToken);

            await trans.CommitAsync(cancellationToken);
        }
    }
}

