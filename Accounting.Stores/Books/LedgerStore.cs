
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Books;

public class LedgerStore : AccountingBaseStore, ILedgerStore
{
    public LedgerStore(AccountingDbContext context) : base(context)
    {

    }

    static readonly string[] insert_fields = ["LedgerId", "PayTime", "FlowDirection", "Amount", "Remark", "CreatedTime", "LastModifiedTime", "AssetAccountId", "CategoryName", "Currency", "SourceChannelCode", "SourceChannelId", "Tags", "TransactionContent", "TransactionCreatedTime", "TransactionParty", "TransactionAccount", "TransactionId", "TransactionMethod", "TransactionStatus", "TransactionType"];
    static readonly string[] update_fields = [ "PayTime", "FlowDirection", "Amount", "Remark", "LastModifiedTime", "CategoryName", "Currency", "TransactionContent", "TransactionCreatedTime", "TransactionParty", "TransactionAccount", "TransactionId", "TransactionMethod", "TransactionStatus", "TransactionType"];

    public async Task BulkSaveChannelRecordsAsync(List<LedgerRecord> data, CancellationToken cancellationToken)
    {
        using (var trans = await this.Context.Database.BeginTransactionAsync())
        {
            await this.Context.BulkInsertOrUpdateAsync(
                data,
                bulkAction: config =>
                {
                    // config.IncludeGraph = true;
                    // config.UpdateByProperties = [nameof(LedgerRecord.SourceChannelCode), nameof(LedgerRecord.SourceChannelId)];
                    config.PropertiesToExcludeOnUpdate = [nameof(LedgerRecord.LedgerId), nameof(LedgerRecord.CreatedTime), nameof(LedgerRecord.Tags)];
                },
                cancellationToken: cancellationToken);

            await trans.CommitAsync(cancellationToken);
        }
    }
}

