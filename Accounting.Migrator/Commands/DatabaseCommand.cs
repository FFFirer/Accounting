using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Org.BouncyCastle.Asn1.X509;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Accounting.Migrator;

public class DatabaseCommand : Command
{
    public DatabaseCommand() : base("database")
    {
        AddCommand(new UpdateCommand());
        AddCommand(new DropCommand());
    }

    public class UpdateCommand : Command
    {
        public UpdateCommand() : base("update")
        {
            this.AddOption(TargetOption);
            this.AddOption(GlobalOptions.DbContextOption);

            this.SetHandler(UpdateDatabase);
        }

        static Option<string?> TargetOption = new Option<string?>("--target", "target migration name");

        private async Task UpdateDatabase(InvocationContext context)
        {
            var logger = context.GetLoggerFactory().CreateLogger("Database.Update");
            var contexts = context.ParseResult.GetValueForOption(GlobalOptions.DbContextOption);
            var connectionName = context.ParseResult.GetValueForOption(GlobalOptions.ConnectionNameOption);
            var target = context.ParseResult.GetValueForOption(TargetOption);

            if (contexts is null) { contexts = new List<string>(); }
            if (contexts.Count <= 0) { contexts.AddRange(Constants.DbContexts.Keys); }

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

                if (dbContext is null)
                {
                    logger.LogWarning("Cannot create instance of db.");
                    //context.Console.FullSingleLine();
                    context.Console.WriteLine("");
                    continue;
                }

                var pendingMigrations = (await dbContext.Database.GetPendingMigrationsAsync(context.GetCancellationToken())).Select(x => new
                {
                    Name = x
                }).ToList();

                if (pendingMigrations.IsNullOrEmpty())
                {
                    context.Console.WriteLine("No pending migrations.");
                }
                else
                {
                    await dbContext.Database.MigrateAsync(target, context.GetCancellationToken());
                    context.FormatToOutput(pendingMigrations, "table");
                }

                //context.Console.FullSingleLine();
                context.Console.WriteLine("");
            }
        }
    }

    public class DropCommand : Command
    {
        public DropCommand() : base("drop", "drop database")
        {
            this.AddOption(GlobalOptions.DbContextOption);

            this.SetHandler(DropDatabase);
        }
        private async Task DropDatabase(InvocationContext context)
        {
            var logger = context.GetLoggerFactory().CreateLogger("Database.Update");
            var contexts = context.ParseResult.GetValueForOption(GlobalOptions.DbContextOption);
            var connectionName = context.ParseResult.GetValueForOption(GlobalOptions.ConnectionNameOption);

            if (contexts is null) { contexts = new List<string>(); }
            if (contexts.Count <= 0) { contexts.AddRange(Constants.DbContexts.Keys); }

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

                if (dbContext is null)
                {
                    logger.LogWarning("Cannot create instance of db.");
                    //context.Console.FullSingleLine();
                    context.Console.WriteLine("");
                    continue;
                }

                await dbContext.Database.EnsureDeletedAsync(context.GetCancellationToken());

                context.Console.WriteLine("Droped.");

                //context.Console.FullSingleLine();
                context.Console.WriteLine("");
            }
        }
    }
}
