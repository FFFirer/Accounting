using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Accounting.Migrator
{
    public class MigrationsCommand : Command
    {
        public MigrationsCommand() : base("migrations")
        {
            AddCommand(new ListCommand());

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
                    context.Console.WriteLine($"[{db}]");
                    context.Console.FullDoubleLine();

                    if (Constants.DbContextFactories.TryGetValue(db, out var factoryType) == false)
                    {
                        logger.LogWarning("No DbContextFactory tpye: {Key}", db);
                        context.Console.FullSingleLine();
                        context.Console.WriteLine("");
                        continue;
                    }

                    var dbContext = Helper.CreateDbContextFromFactory(factoryType, connectionName);

                    if(dbContext is null)
                    {
                        logger.LogWarning("Cannot instance db.");
                        context.Console.FullSingleLine();
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

                        context.Console.FullSingleLine();
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

                        context.Console.FullSingleLine();
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
                            Applied = applied.ContainsKey(x) ? "√" : ""
                        }).ToList();

                        if(migrations.IsNullOrEmpty())
                        {
                            context.Console.WriteLine("Empty.");
                        }
                        else
                        {
                            context.FormatToOutput(migrations, "table");
                        }

                        context.Console.FullSingleLine();
                        context.Console.WriteLine("");
                    }
                }
            }
        }
    }
}
