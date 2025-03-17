using System;

using Accounting.Asset;

namespace Accounting.Books;

public class Ledger
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTimeOffset CreatedTime { get; set; }

    /// <summary>
    /// 月初起始日
    /// </summary>
    public int MonthStartDay { get; set; }

    public List<LedgerCategory> Categories { get; set; } = [];
    public List<LedgerTag> Tags { get; set; } = [];

    public ICollection<LedgerRecord> Records { get; set; } = [];

    public List<AssetAccount>? AssetAccounts { get; set; }
}
