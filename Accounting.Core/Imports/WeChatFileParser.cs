using Accounting.Asset;
using Accounting.Books;
using Accounting.Data;
using Accounting.Imports;
using CsvHelper.Configuration;

namespace Accounting.Imports;

public class WeChatFileParser : IChannelFileParser
{
    protected virtual ImportErrorDescriber ErrorDescriber { get; set; }
    protected virtual CsvFileReader CsvFile { get; set; }

    public WeChatFileParser(ImportErrorDescriber errorDescriber, CsvFileReader csvFile)
    {
        this.ErrorDescriber = errorDescriber;
        this.CsvFile = csvFile;
    }

    public async Task<Result<List<LedgerRecord>>> ParseAsync(ImportRecord record, CancellationToken cancellationToken)
    {
        if (record.File?.StoragePath is null)
        {
            return Result<List<LedgerRecord>>.Failed(this.ErrorDescriber.NoFile());
        }

        if (File.Exists(record.File.StoragePath) == false)
        {
            return Result<List<LedgerRecord>>.Failed(this.ErrorDescriber.FileNotExists(record.File.StoragePath));
        }

        var records = await CsvFile.ParseAsync<LedgerRecord>(record.File.StoragePath, 4, new WeChatLedgerRecordMap(), cancellationToken);

        return Result.Success(records);
    }

    private static AssetFlowDirection ParseFlowDirection(string name)
    {
        return name switch
        {
            "收入" => AssetFlowDirection.Revenue,
            "支出" => AssetFlowDirection.Expenditure,
            _ => AssetFlowDirection.Ignore
        };
    }
private static DateTimeOffset ParseToLocalTime(string v)
    {
        return DateTimeOffset.Parse(v);
    }
    private class WeChatLedgerRecordMap : ClassMap<LedgerRecord>
    {
        public WeChatLedgerRecordMap()
        {
            Map(x => x.FlowDirection).Convert(args => ParseFlowDirection(args.Row["收/支"]!));
            Map(x => x.Amount).Convert(args => Decimal.Parse(args.Row["金额(元)"]!));
            Map(x => x.Currency).Constant("人民币");
            Map(x => x.Remark).Name("备注");
            Map(x => x.CategoryName).Name("交易类型");
            Map(x => x.TransactionId).Name("商户单号");
            Map(x => x.TransactionParty).Name("交易对方");
            Map(x => x.TransactionContent).Name("商品");
            Map(x => x.TransactionMethod).Name("支付方式");
            Map(x => x.TransactionStatus).Name("当前状态");
            Map(x => x.PayTime).Convert(args => ParseToLocalTime(args.Row["交易时间"]!));
            Map(x => x.SourceChannelCode).Constant(ImportChannels.WeChat.Code);
            Map(x => x.SourceChannelId).Name("交易订单号");
            Map(x => x.CreatedTime).Constant(DateTimeOffset.UtcNow);
            Map(x => x.LastModifiedTime).Constant(DateTimeOffset.UtcNow);
        }
    }
}