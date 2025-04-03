using System.Globalization;
using System.Text;
using Accounting.Data;
using CsvHelper;
using CsvHelper.Configuration;

namespace Accounting.Imports;

public class CsvFileReader
{
    public async Task<Result> CheckHeaders(string filePath, int skipRowsCount, CancellationToken cancellationToken, Action<CsvConfiguration>? configure = null, params string[]? checkHeaders) 
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
            
            configure?.Invoke(configuration);

            var reader = new CsvReader(streamReader, configuration);

            await reader.ReadAsync();
            reader.ReadHeader();

            var header = reader.HeaderRecord;

            var count = checkHeaders?.Intersect(header ?? []).Count();

            if(count != checkHeaders?.Length) {
                return Result.Failed(new Error("InvalidCsvHeaders", "Csv文件列信息不匹配"));
            }
        }

        return Result.Success();
    }

    public async Task<List<T>> ParseAsync<T>(string filePath, int skipRowsCount, ClassMap<T>? map, CancellationToken cancellationToken, Action<CsvConfiguration>? configure = null)
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
            
            configure?.Invoke(configuration);

            var reader = new CsvReader(streamReader, configuration);

            if (map is not null)
            {
                reader.Context.RegisterClassMap(map);
            }

            await reader.ReadAsync();
            reader.ReadHeader();

            var header = reader.HeaderRecord;

            return reader.GetRecords<T>().ToList();
        }
    }
}