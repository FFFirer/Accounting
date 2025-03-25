using Accounting.Data;

namespace Accounting.Imports;

public interface IChannelFileParser {
    Task<Result> ParseAsync(ImportRecord record, CancellationToken cancellationToken);
}