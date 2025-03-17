using System;

namespace Accounting.Books;

public class LedgerTag
{
    public LedgerTag(LedgerRecord record, string name)
    {
        Record = record;
        CreatedTime = DateTimeOffset.UtcNow;
        Name = name;
    }

    public long Id { get; set; }
    public LedgerRecord Record { get; set; }
    public string Name { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
}
