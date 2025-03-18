using System;

using Accounting.Books;

namespace Accounting.Asset;

public class AssetAccount
{
    public AssetAccount() : this("", "")
    {

    }

    public AssetAccount(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; set; }

    public string Name { get; set; }
    public AssetAccountType? Type { get; set; }

    public string? Icon { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public string? Remark { get; set; }

    public bool IncludedInTotalAssets { get; set; }

    public ICollection<LedgerRecord>? Records { get; set; }

    public ICollection<Ledger>? Ledgers { get; set; }
}
