using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

using Microsoft.Extensions.Logging;

using Serilog;

using Serilog.Events;

namespace Accounting.Migrator.Extensions;


public static class LoggerExtensions
{
    public static Serilog.ILogger CreateLogger(this IConsole console, LogLevel minimumLevel = LogLevel.Warning)
    {
        var loggerConfiguration = new LoggerConfiguration()
            .MinimumLevel.Is(minimumLevel switch
            {
                LogLevel.Error => LogEventLevel.Error,
                LogLevel.Warning => LogEventLevel.Warning,
                LogLevel.Information => LogEventLevel.Information,
                LogLevel.Debug => LogEventLevel.Debug,
                LogLevel.Trace => LogEventLevel.Verbose,
                LogLevel.Critical => LogEventLevel.Fatal,
                _ => LogEventLevel.Fatal,
            })
            .WriteTo.Console()
            .Enrich.FromLogContext();

        return loggerConfiguration.CreateLogger();
    }
}
