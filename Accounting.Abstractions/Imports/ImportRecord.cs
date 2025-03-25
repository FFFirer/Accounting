using Accounting.FileStorage;

namespace Accounting.Imports;

public class ImportRecord
{
    public long Id { get; set; }

    public FileInformation? File { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public ImportChannel? Channel { get; set; }

    public ImportStatus? Status { get; set; }

    public int Count { get; set; }
}