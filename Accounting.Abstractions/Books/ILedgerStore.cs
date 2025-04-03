using System;
using Accounting.Imports;

namespace Accounting.Books;

public interface ILedgerStore
{
    Task BulkSaveChannelRecordsAsync(List<LedgerRecord> data, CancellationToken cancellationToken);
    Task CreateAsync(ImportRecord record, CancellationToken cancellationToken);
}
