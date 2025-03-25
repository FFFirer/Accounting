using Accounting.Data;

namespace Accounting.Imports;

public class ImportErrorDescriber
{
    public Error FileNotExists(string? filePath) => new Error(nameof(FileNotExists), $"文件不存在：{filePath}");

    public Error NoFile() => new Error(nameof(NoFile), "没有文件");
}