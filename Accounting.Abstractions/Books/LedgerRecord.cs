using System;

using Accounting.Asset;
using Accounting.Common;

namespace Accounting.Books;

public class LedgerRecord
{
    public LedgerRecord(
        DateTimeOffset recordTime,
        decimal amount)
    {
        RecordTime = recordTime;
        Amount = amount;
        CreatedTime = DateTimeOffset.UtcNow;
        LastModifiedTime = DateTimeOffset.UtcNow;
    }

    public long Id { get; set; }
    public Ledger Ledger { get; set; } = default!;
    public LedgerCategory Category { get; set; } = default!;
    public ICollection<LedgerTag>? Tags { get; set; }

    public DateTimeOffset RecordTime { get; set; }
    public AssetFlowDirection FlowDirection { get; set; }

    public decimal Amount { get; set; }


    public string? Remark { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastModifiedTime { get; set; }

    public ICollection<LedgerRecordAttachment>? Attachments { get; set; }

    public AssetAccount? AssetAccount { get; set; }
}
