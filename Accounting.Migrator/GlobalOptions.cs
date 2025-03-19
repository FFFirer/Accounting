using System.CommandLine;

using Microsoft.Extensions.Logging;

namespace Accounting.Migrator
{
    public class GlobalOptions
    {
        public static Option<string> ConnectionNameOption = new Option<string>("--connection-name");

        public static Option<List<string>> DbContextOption = new Option<List<string>>(new[] { "--context", "-c" })
        {
            Arity = ArgumentArity.ZeroOrMore,
        }.AddCompletions(Constants.DbContextFactories.Keys.ToArray());

        public static string[] FormatValues = new[] { "json", "table" };
        public static Option<string> FormatOption = new Option<string>(new[] { "--format", "-f" },
                                                                       "Output as format")
            .FromAmong(FormatValues!)
            .AddCompletions(FormatValues!);

        public static string[] LogLevelValues = new string[]
        {
            "None",
            "Warning",
            "Debug",
            "Information",
            "Trace",
            "Error",
            "Critical"
        };
        public static Option<LogLevel> LogLevelOption = new Option<LogLevel>(new[] { "--loglevel" },
                                                                         description: "Log level",
                                                                         getDefaultValue: () => LogLevel.Warning)
        .FromAmong(LogLevelValues);
    }
}