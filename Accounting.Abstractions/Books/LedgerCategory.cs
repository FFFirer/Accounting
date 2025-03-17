using System;

namespace Accounting.Books;

public class LedgerCategory
{
    public LedgerCategory(Ledger ledger, string name)
    {
        Ledger = ledger;
        Name = name;
        CreatedTime = DateTimeOffset.UtcNow;
    }

    public long Id { get; set; }
    public Ledger Ledger { get; set; }
    public string Name { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

}
