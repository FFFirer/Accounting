using System.Dynamic;
using System.Globalization;
using System.Text;
using Accounting.Asset;
using Accounting.Books;
using Accounting.Common;
using Accounting.Data;
using Accounting.Imports;
using CsvHelper;
using CsvHelper.Configuration;

namespace Accounting.Imports;

public class AlipayFileParser : IChannelFileParser
{
    protected virtual ImportErrorDescriber ErrorDescriber { get; set; }
    protected virtual CsvFileReader CsvFile { get; set; }

    public AlipayFileParser(ImportErrorDescriber errorDescriber, CsvFileReader csvFile)
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

        var records = await CsvFile.ParseAsync<LedgerRecord>(record.File.StoragePath, 24, new AlipayLedgerRecordMap(), cancellationToken);

        return Result.Success(records);
    }

    private static AssetFlowDirection ParseFlowDirection(string? name)
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

    public class AlipayLedgerRecordMap : ClassMap<LedgerRecord>
    {
        public AlipayLedgerRecordMap()
        {
            Map(x => x.FlowDirection).Convert(args => ParseFlowDirection(args.Row["收/支"]!));
            Map(x => x.Amount).Convert(args => Decimal.Parse(args.Row["金额"]!));
            Map(x => x.Currency).Constant("人民币");
            Map(x => x.Remark).Name("备注");
            Map(x => x.CategoryName).Name("交易分类");
            Map(x => x.TransactionId).Name("商家订单号");
            Map(x => x.TransactionParty).Name("交易对方");
            Map(x => x.TransactionAccount).Name("对方账号");
            Map(x => x.TransactionContent).Name("商品说明");
            Map(x => x.TransactionMethod).Name("收/付款方式");
            Map(x => x.TransactionStatus).Name("交易状态");
            Map(x => x.PayTime).Convert(args => ParseToLocalTime(args.Row["交易时间"]!));
            Map(x => x.SourceChannelCode).Constant(ImportChannels.Alipay.Code);
            Map(x => x.SourceChannelId).Name("交易订单号");
            Map(x => x.CreatedTime).Constant(DateTimeOffset.UtcNow);
            Map(x => x.LastModifiedTime).Constant(DateTimeOffset.UtcNow);
        }

    }
}