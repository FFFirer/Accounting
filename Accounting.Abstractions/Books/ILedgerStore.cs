using System;

namespace Accounting.Books;

public interface ILedgerStore
{
    Task BulkSaveChannelRecordsAsync(List<LedgerRecord> data, CancellationToken cancellationToken);
}
