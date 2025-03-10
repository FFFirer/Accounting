using System.CommandLine;
using System.CommandLine.Invocation;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Accounting.Migrator
{
    public class Root : RootCommand
    {
        public Root()
        {
            this.AddGlobalOption(GlobalOptions.ConnectionNameOption);

            this.AddCommand(new ListCommand());
            this.AddCommand(new UpdateCommand());
            this.AddCommand(new DropCommand());
        }

        static DbContext BuildDbContext(string? connectionName)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationBuilder.AddUserSecrets<Root>();
            configurationBuilder.AddEnvironmentVariables(prefix: "Accounting_");

            var configuration = configurationBuilder.Build();

            var factory = new DesignTimeDbContextFactory(configuration);

            return factory.CreateDbContext(new DesignTimeDbContextFactory.FactoryOptions
            {
                ConnectionString = configuration.GetConnectionString(connectionName ?? "Default")
            });
        }

        public class ListCommand : Command
        {
            static Option<bool> PendingOption = new Option<bool>("--pending", "list pending migrations");
            static Option<bool> NoConnectOption = new Option<bool>("--no-connect", "without connection");

            public ListCommand() : base("list", "list migrations")
            {
                this.AddOption(PendingOption);
                this.AddOption(NoConnectOption);

                this.SetHandler(Handle);
            }

            private async Task Handle(InvocationContext context)
            {
                var connectionName = context.ParseResult.GetValueForOption(GlobalOptions.ConnectionNameOption);

                var pending = context.ParseResult.GetValueForOption(PendingOption);
                var noConnect = context.ParseResult.GetValueForOption(NoConnectOption);

                var db = BuildDbContext(connectionName);

                if (noConnect)
                {
                    foreach (var m in db.Database.GetMigrations())
                    {
                        context.Console.WriteLine(m);
                    }
                    return;
                }

                if (pending)
                {
                    foreach (var m in await db.Database.GetPendingMigrationsAsync(context.GetCancellationToken()))
                    {
                        context.Console.WriteLine(m);
                    }
                    return;
                }

                var applied = (await db.Database.GetAppliedMigrationsAsync(context.GetCancellationToken())).GroupBy(x => x).ToDictionary(x => x.Key, y => y.First());

                foreach (var m in db.Database.GetMigrations())
                {
                    context.Console.WriteLine($"{(applied.ContainsKey(m) ? "√" : " ")}\t{m}");
                }

                return;
            }

        }

        public class UpdateCommand : Command
        {
            static Option<string?> TargetOption = new Option<string?>("--target", "target migration name");

            public UpdateCommand() : base("update", "update database")
            {
                this.AddOption(TargetOption);

                this.SetHandler(Handle);
            }

            private async Task Handle(InvocationContext context)
            {
                var connectionName = context.ParseResult.GetValueForOption(GlobalOptions.ConnectionNameOption);

                var target = context.ParseResult.GetValueForOption(TargetOption);

                var db = BuildDbContext(connectionName);

                await db.Database.MigrateAsync(target, context.GetCancellationToken());

                foreach (var m in await db.Database.GetAppliedMigrationsAsync(context.GetCancellationToken()))
                {
                    context.Console.WriteLine(m);
                }
            }
        }

        public class DropCommand : Command
        {
            public DropCommand() : base("drop", "drop database")
            {
                this.SetHandler(Handle);
            }
            private async Task Handle(InvocationContext context)
            {
                var connectionName = context.ParseResult.GetValueForOption(GlobalOptions.ConnectionNameOption);

                var db = BuildDbContext(connectionName);

                await db.Database.EnsureDeletedAsync(context.GetCancellationToken());

                context.Console.WriteLine("Database dropped!");
            }
        }
    }
}