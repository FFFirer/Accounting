using Accounting.Asset;
using Accounting.Books;
using Accounting.Data;
using Accounting.Imports;
using CsvHelper;
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

        var checkResults = await CsvFile.CheckHeaders(record.File.StoragePath, 16, cancellationToken, checkHeaders: headers);

        switch (checkResults)
        {
            case { Succeeded: true }:
                var records = await CsvFile.ParseAsync<LedgerRecord>(record.File.StoragePath, 16, new WeChatLedgerRecordMap(), cancellationToken, ConfigureCsvConfiguration);
                return records;
                
            default:
                return Result<List<LedgerRecord>>.Failed(checkResults.Errors);
        }
    }

    private void ConfigureCsvConfiguration(CsvConfiguration configuration)
    {
        configuration.DetectDelimiterValues = [","];
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

    static readonly string[] headers = ["收/支", "金额(元)", "人民币", "备注", "交易类型", "商户单号", "交易对方", "商品", "支付方式", "当前状态", "交易时间", "交易单号"];

    private class WeChatLedgerRecordMap : ClassMap<LedgerRecord>
    {
        public WeChatLedgerRecordMap()
        {
            Map(x => x.FlowDirection).Convert(args => ParseFlowDirection(args.Row["收/支"]!));
            Map(x => x.Amount).Convert(args => Decimal.Parse(args.Row["金额(元)"]![1..]));
            Map(x => x.Currency).Constant("人民币");
            Map(x => x.Remark).Convert(args =>
            {
                return args.Row["备注"];
            });
            Map(x => x.CategoryName).Name("交易类型");
            Map(x => x.TransactionId).Convert(args => args.Row["商户单号"]?.Trim());
            Map(x => x.TransactionParty).Name("交易对方");
            Map(x => x.TransactionContent).Name("商品");
            Map(x => x.TransactionMethod).Name("支付方式");
            Map(x => x.TransactionStatus).Name("当前状态");
            Map(x => x.PayTime).Convert(args => ParseToLocalTime(args.Row["交易时间"]!));
            Map(x => x.SourceChannelCode).Constant(ImportChannels.WeChat.Code);
            Map(x => x.SourceChannelId).Convert(args => args.Row["交易单号"]?.Trim());
            Map(x => x.CreatedTime).Constant(DateTimeOffset.UtcNow);
            Map(x => x.LastModifiedTime).Constant(DateTimeOffset.UtcNow);
        }
    }
}