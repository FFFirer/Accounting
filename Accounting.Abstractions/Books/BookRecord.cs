using System;

using Accounting.Common;

namespace Accounting.Books;

public class BookRecord
{
    public BookRecord(string id, DateTimeOffset recordTime, FinancialFlowDirection flowDirection, decimal? amount)
    {
        this.Id = id;
        this.FlowDirection = flowDirection;
        this.Amount = amount;
        this.RecordTime = recordTime;

        this.CreatedTime = DateTimeOffset.UtcNow;
        this.LastModifiedTime = DateTimeOffset.UtcNow
    }

    public string Id { get; set; }

    public BookEntity? Book { get; set; }

    public FinancialFlowDirection FlowDirection { get; set; }

    public DateTimeOffset RecordTime { get; set; }

    public decimal? Amount { get; set; }

    public string? Remark { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset LastModifiedTime { get; set; }

    public BookRecordCategory? Category { get; set; }

    public List<BookRecordTag>? Tags { get; set; }
}
