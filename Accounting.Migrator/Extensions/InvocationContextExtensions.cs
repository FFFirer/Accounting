using System;
using System.Collections;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;
using System.Text.Json;

using Accounting.Migrator.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Serilog;

namespace Accounting.Migrator;

public static class InvocationContextExtensions
{

    public static void FormatToOutput<T>(this InvocationContext context, T value, string? defaultFormat = "")
    {
        var formatOption = context.ParseResult.GetValueForOption(GlobalOptions.FormatOption);

        if (string.IsNullOrWhiteSpace(formatOption))
        {
            formatOption = defaultFormat;
        }

        //if (string.Equals(formatOption, "json", StringComparison.OrdinalIgnoreCase))
        //{
        //    var json = JsonSerializer.Serialize(value);
        //    context.Console.WriteLine(json);
        //    return;
        //}

        if (string.Equals(formatOption, "json", StringComparison.OrdinalIgnoreCase) && typeof(T).GetInterfaces().Contains(typeof(IEnumerable)))
        {
            // TODO: TUI table
            var rows = (IEnumerable?)value;

            if (rows is not null)
            {
                foreach (var row in rows)
                {
                    context.Console.WriteLine(JsonSerializer.Serialize(row));
                }
            }

            return;
        }

        if (string.Equals(formatOption, "table", StringComparison.OrdinalIgnoreCase) && typeof(T).GetInterfaces().Contains(typeof(IEnumerable)))
        {
            context.Console.PrintAsTable(value);

            return;
        }

        context.Console.WriteLine(value?.ToString() ?? "<NULL>");
    }

    public static Serilog.ILogger CreateSerilogLogger(this InvocationContext context)
    {
        var logLevel = context.ParseResult.GetValueForOption(GlobalOptions.LogLevelOption);

        return context.Console.CreateLogger(logLevel);
    }

    public static ILoggerFactory GetLoggerFactory(this InvocationContext context)
    {

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddSerilog(context.CreateSerilogLogger());
        });

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        return loggerFactory;
    }

}
