using System;

namespace Accounting.Books;

public class LedgerCategory
{
    public LedgerCategory(string name)
    {
        Name = name;
        CreatedTime = DateTimeOffset.UtcNow;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public Guid? LedgerId { get; set; }
    public DateTimeOffset CreatedTime { get; set; }

}
