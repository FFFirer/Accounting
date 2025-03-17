using System;

namespace Accounting.Books;

public class LedgerTag
{
    public LedgerTag(string name)
    {
        CreatedTime = DateTimeOffset.UtcNow;
        Name = name;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public Guid LedgerId { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
}
