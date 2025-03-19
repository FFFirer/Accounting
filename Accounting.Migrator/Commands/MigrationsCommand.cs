using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;

namespace Accounting.Migrator
{
    public class MigrationsCommand : Command
    {
        public MigrationsCommand() : base("migrations")
        {
            AddCommand(new ListCommand());
            AddCommand(new ScriptCommand());

            AddOption(GlobalOptions.DbContextOption);
        }

        public class ListCommand : Command {

            static Option<bool> PendingOption = new Option<bool>("--pending", "list pending migrations");
            static Option<bool> NoConnectOption = new Option<bool>("--no-connect", "without connection");

            public ListCommand() : base("list")
            {
                this.AddOption(PendingOption);
                this.AddOption(NoConnectOption);
                this.AddOption(GlobalOptions.DbContextOption);

                this.SetHandler(ListMigrations);
            }

            private async Task ListMigrations(InvocationContext context)
            {
                var logger = context.GetLoggerFactory().CreateLogger("Migrations.List");
                var contexts = context.ParseResult.GetValueForOption(GlobalOptions.DbContextOption);
                var connectionName = context.ParseResult.GetValueForOption(GlobalOptions.ConnectionNameOption);
                //var format = context.ParseResult.GetValueForOption(GlobalOptions.FormatOption) ?? "table";
                var pending = context.ParseResult.GetValueForOption(PendingOption);
                var noConnect = context.ParseResult.GetValueForOption(NoConnectOption);

                if(contexts is null) { contexts = new List<string>(); }
                if(contexts.Count <= 0) { contexts.AddRange(Constants.DbContexts.Keys); }

                foreach (var db in contexts)
                {
                    context.Console.FullSingleLine();
                    context.Console.WriteLine(db);
                    context.Console.FullDoubleLine();

                    if (Constants.DbContextFactories.TryGetValue(db, out var factoryType) == false)
                    {
                        logger.LogWarning("No DbContextFactory tpye: {Key}", db);
                        //context.Console.FullSingleLine();
                        context.Console.WriteLine("");
                        continue;
                    }

                    using var dbContext = Helper.CreateDbContextFromFactory(factoryType, connectionName);

                    if(dbContext is null)
                    {
                        logger.LogWarning("Cannot create instance of db.");
                        //context.Console.FullSingleLine();
                        context.Console.WriteLine("");
                        continue;
                    }

                    if(noConnect)
                    {
                        var migrations = dbContext.Database.GetMigrations().Select(x => new { Name = x }).ToList();
                        
                        if(migrations.IsNullOrEmpty())
                        {
                            context.Console.WriteLine("Empty.");
                        }
                        else
                        {
                            context.FormatToOutput(migrations, "table");
                        }

                        //context.Console.FullSingleLine();
                        context.Console.WriteLine("");
                        continue;
                    }

                    if (pending)
                    {
                        var migrations = (await dbContext.Database.GetPendingMigrationsAsync(context.GetCancellationToken()))
                            .Select(x => new
                            {
                                Name = x
                            })
                            .ToList();

                        if (migrations.IsNullOrEmpty())
                        {
                            context.Console.WriteLine("Empty.");
                        }
                        else
                        {
                            context.FormatToOutput(migrations, "table");
                        }

                        //context.Console.FullSingleLine();
                        context.Console.WriteLine("");
                        continue;
                    }

                    var applied = ((await dbContext.Database.GetAppliedMigrationsAsync(context.GetCancellationToken()))
                        .GroupBy(x => x)
                        .ToDictionary(x => x.Key, y => y.First()
                        ));

                    {
                        var migrations = dbContext.Database.GetMigrations().Select(x => new
                        {
                            Name = x,
                            Applied = applied.ContainsKey(x) ? "YES" : ""
                        }).ToList();

                        if(migrations.IsNullOrEmpty())
                        {
                            context.Console.WriteLine("Empty.");
                        }
                        else
                        {
                            context.FormatToOutput(migrations, "table");
                        }

                        //context.Console.FullSingleLine();
                        context.Console.WriteLine("");
                    }
                }
            }
        }
    
        public class ScriptCommand : Command
        {
            static Argument<string> FROM = new Argument<string>("FROM", getDefaultValue: () => "0")
            {
                Arity = ArgumentArity.ZeroOrOne
            };
            static Argument<string> TO = new Argument<string>("TO")
            {
                Arity = ArgumentArity.ZeroOrOne
            };

            static Option<string> OutputOption = new Option<string>(new[] { "-o", "--output" }, description: "output file");
            static Option<bool> SplitOption = new Option<bool>("--split", description: "Split multi-context outputs");
            static Option<MigrationsSqlGenerationOptions> GenerationOption = new Option<MigrationsSqlGenerationOptions>("--generation-options", getDefaultValue: () => MigrationsSqlGenerationOptions.Default)
                .AddCompletions(["Default", "NoTransactions", "Idempotent", "Script"]);
            
            public ScriptCommand(): base("script")
            {
                AddArgument(FROM); 
                AddArgument(TO);

                AddOption(GlobalOptions.DbContextOption);
                AddOption(OutputOption);
                AddOption(SplitOption);
                AddOption(GenerationOption);

                this.SetHandler(ScriptMigrations);
            }

            private async Task ScriptMigrations(InvocationContext context)
            {
                var logger = context.GetLoggerFactory().CreateLogger("Migrations.Script");

                var fromMigrationName = context.ParseResult.GetValueForArgument(FROM);
                var toMigrationName = context.ParseResult.GetValueForArgument(TO);
                var output = context.ParseResult.GetValueForOption(OutputOption);
                var split = context.ParseResult.GetValueForOption(SplitOption);
                var contexts = context.ParseResult.GetValueForOption(GlobalOptions.DbContextOption);
                var generationOption = context.ParseResult.GetValueForOption(GenerationOption);
                if(contexts is null) { contexts = new List<string>(); }
                if(contexts.IsNullOrEmpty()) { contexts.AddRange(Constants.DbContexts.Keys); }

                if(string.IsNullOrWhiteSpace(output))
                {
                    output = "./script.sql";
                }

                var fullFilePath = Path.IsPathRooted(output) ? output : Path.Combine(AppContext.BaseDirectory, output);
                
                var fileName = Path.GetFileName(fullFilePath); 
                var directoryPath = Path.GetDirectoryName(fullFilePath); 
                var directory = Directory.Exists(directoryPath) ? new DirectoryInfo(directoryPath) : Directory.CreateDirectory(directoryPath!);

                if(split)
                {
                    var extension = Path.GetExtension(fullFilePath);
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullFilePath);

                    fileName = $"{fileNameWithoutExtension}-{{0}}{extension}";
                }

                foreach (var db in contexts)
                {
                    context.Console.FullSingleLine();
                    context.Console.WriteLine(db);
                    context.Console.FullDoubleLine();

                    if(Constants.DbContextFactories.TryGetValue(db, out var factory) == false)
                    {
                        logger.LogWarning("No DbContextFactory type: {Key}", db);
                        context.Console.WriteLine("");
                        continue;
                    }

                    using var dbContext = Helper.CreateDbContextFromFactory(factory, null);

                    if(dbContext is null)
                    {
                        logger.LogWarning("Cannot create instance of db.");
                        //context.Console.FullSingleLine();
                        context.Console.WriteLine("");
                        continue;
                    }

                    var migrator = dbContext.GetService<IMigrator>();

                    var script = migrator.GenerateScript(fromMigrationName, toMigrationName, generationOption);

                    var scriptFileName = split ? string.Format(fileName, db) : fileName;
                    var scriptFilePath = Path.Combine(directory.FullName, scriptFileName);

                    using var fs = new FileStream(scriptFilePath, FileMode.OpenOrCreate, FileAccess.Write);
                    using var writer = new StreamWriter(fs, Encoding.UTF8);
                    await writer.WriteAsync($"-- DbContext: {db}\r\n\r\n");
                    await writer.WriteAsync(script);
                    await writer.WriteAsync($"\r\n\r\n-- DbContext: {db} End\r\n\r\n");

                    context.Console.WriteLine($"Generated into {scriptFilePath}");
                    context.Console.WriteLine("");
                }
            }
        }
    }
}
