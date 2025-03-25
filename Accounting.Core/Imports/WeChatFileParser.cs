using Accounting.Data;
using Accounting.Imports;

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

    public async Task<Result> ParseAsync(ImportRecord record, CancellationToken cancellationToken)
    {
        if (record.File?.StoragePath is null)
        {
            return Result.Failed(this.ErrorDescriber.NoFile());
        }

        if (File.Exists(record.File.StoragePath) == false)
        {
            return Result.Failed(this.ErrorDescriber.FileNotExists(record.File.StoragePath));
        }

        var rows = await CsvFile.ParseAsync<dynamic>(record.File.StoragePath, 4, cancellationToken);

        foreach (var row in rows)
        {
            var item = new ImportRecordItem();

            record.Items.Add(item);
        }

        return Result.Success();
    }
}