using System.Globalization;
using System.Text;
using Accounting.Data;
using CsvHelper;
using CsvHelper.Configuration;

namespace Accounting.Imports;

public class CsvFileReader
{
    public async Task<List<T>> ParseAsync<T>(string filePath, int skipRowsCount, CancellationToken cancellationToken)
    {
        var encoding = Encoding.UTF8;

        using (var fs = File.OpenRead(filePath))
        {
            var detect = await ImportHelper.DetectEncodingAsync(fs, cancellationToken);

            encoding = detect.Detected.Encoding;
        }

        using (var fs = File.OpenRead(filePath))
        {
            var streamReader = new StreamReader(fs, encoding);

            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
            configuration.TrimOptions = TrimOptions.Trim;
            configuration.ShouldSkipRecord += (args) => args.Row.Parser.RawRow <= skipRowsCount;

            var reader = new CsvReader(streamReader, configuration);

            await reader.ReadAsync();
            reader.ReadHeader();

            var header = reader.HeaderRecord;

            return reader.GetRecords<T>().ToList();
        }
    }
}