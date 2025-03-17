using System;

using Accounting.Books;

namespace Accounting.Asset;

public class AssetAccount
{
    public AssetAccount(string id, string name, string type)
    {
        Id = id;
        Name = name;
        Type = type;
    }

    public string Id { get; set; }

    public string Name { get; set; }
    public string Type { get; set; }

    public string? Icon { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public string? Remark { get; set; }

    public bool IncludedInTotalAssets { get; set; }

    public ICollection<LedgerRecord>? Records { get; set; }
}
