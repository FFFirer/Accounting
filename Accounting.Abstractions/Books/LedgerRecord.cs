using System;

using Accounting.Asset;
using Accounting.Common;

namespace Accounting.Books;

public class LedgerRecord
{
    public LedgerRecord() : this("Custom", Guid.CreateVersion7().ToString(), 0, "") { }

    public LedgerRecord(
        string sourceCode,
        string sourceId,
        decimal amount,
        string currency)
    {
        Amount = amount;
        Currency = currency;
        CreatedTime = DateTimeOffset.UtcNow;
        LastModifiedTime = DateTimeOffset.UtcNow;
        SourceChannelCode = sourceCode;
        SourceChannelId = sourceId;
    }

    /// <summary>
    /// 所属账本
    /// </summary>
    public Guid? LedgerId { get; set; }

    /// <summary>
    /// 分类
    /// </summary>
    public string? CategoryName { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    public string[]? Tags { get; set; }

    /// <summary>
    /// 收/支
    /// </summary>
    public AssetFlowDirection? FlowDirection { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    public decimal? Amount { get; set; }

    /// <summary>
    /// 货币单位
    /// </summary>
    public string? Currency { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 附件
    /// </summary>
    public ICollection<LedgerRecordAttachment>? Attachments { get; set; }

    /// <summary>
    /// 所属资产账户
    /// </summary>
    public AssetAccount? AssetAccount { get; set; }

    public string? TransactionType { get; set; }

    public string? TransactionId { get; set; }

    /// <summary>
    /// 交易方
    /// </summary>
    public string? TransactionParty { get; set; }

    /// <summary>
    /// 交易方账号
    /// </summary>
    public string? TransactionAccount { get; set; }

    /// <summary>
    /// 来源渠道Id
    /// </summary>
    public string? TransactionContent { get; set; }

    /// <summary>
    /// 交易创建时间
    /// </summary>
    public string? TransactionCreatedTime { get; set; }

    /// <summary>
    /// 交易方式
    /// </summary>
    public string? TransactionMethod { get; set; }

    /// <summary>
    /// 交易状态
    /// </summary>
    public string? TransactionStatus { get; set; }

    /// <summary>
    /// 付款时间
    /// </summary>
    public DateTimeOffset? PayTime { get; set; }

    /// <summary>
    /// 来源
    /// </summary>
    public string SourceChannelCode { get; set; }

    /// <summary>
    /// 来源Id
    /// </summary>
    public string SourceChannelId { get; set; }

    // 审计
    public DateTimeOffset? CreatedTime { get; set; }
    public DateTimeOffset? LastModifiedTime { get; set; }
}
