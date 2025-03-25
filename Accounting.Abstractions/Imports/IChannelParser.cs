using Accounting.Books;
using Accounting.Data;

namespace Accounting.Imports;

public interface IChannelFileParser {
    Task<Result<List<LedgerRecord>>> ParseAsync(ImportRecord record, CancellationToken cancellationToken);
}