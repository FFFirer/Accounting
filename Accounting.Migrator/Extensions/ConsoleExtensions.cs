using System.CommandLine;

namespace Accounting.Migrator;

public static class ConsoleExtensions
{
    public static void FullDoubleLine(this IConsole console, string prefix = "") => FullWrite(console, '=', prefix);
    public static void FullSingleLine(this IConsole console, string prefix = "") => FullWrite(console, '-', prefix);
    public static void FullWrite(this IConsole console, char c, string prefix = "")
    {
        var count = prefix.Length;

        console.Write(prefix);
        console.WriteLine(string.Join("", Enumerable.Range(0, Console.WindowWidth - count).Select(index => c)));
    }
}
