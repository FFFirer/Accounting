using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using CommandLine;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Accounting.Quartz.Migrations;

public class AccountingQuartzDbContextFactory : IDesignTimeDbContextFactory<AccountingQuartzDbContext>
{
    public class FactoryOptions
    {
        [Option("--connection-name")]
        public string ConnectionName { get; set; } = "Default";

        [Option("--connection-string")]
        public string? ConnectionString { get; set; }
    }

    public AccountingQuartzDbContextFactory()
    {

    }

    private IConfiguration? Configuration { get; set; }
    public AccountingQuartzDbContextFactory(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration BuildConfiguration()
    {
        if (Configuration is null)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationBuilder.AddUserSecrets<AccountingQuartzDbContextFactory>();
            configurationBuilder.AddEnvironmentVariables(prefix: "Accounting_");

            Configuration = configurationBuilder.Build();
        }

        return Configuration;
    }

    public AccountingQuartzDbContext CreateDbContext(FactoryOptions factoryOptions)
    {
        var contextOptionsBuilder = new DbContextOptionsBuilder<AccountingQuartzDbContext>();
        contextOptionsBuilder.UseNpgsql(
            factoryOptions.ConnectionString,
            options =>
            {
                options.MigrationsAssembly("Accounting.Quartz.Migrations");
                options.MigrationsHistoryTable("__AccountingQuartzMigrationHistory");
            });

        var contextOptions = contextOptionsBuilder.Options;
        var context = new AccountingQuartzDbContext(contextOptions);

        return context;
    }

    public AccountingQuartzDbContext CreateDbContext(string[] args)
    {
        FactoryOptions options = new FactoryOptions();
        Parser.Default.ParseArguments<FactoryOptions>(args).WithParsed(opts => options = opts);

        if (string.IsNullOrWhiteSpace(options.ConnectionString))
        {
            var configuration = BuildConfiguration();

            options.ConnectionString = configuration.GetConnectionString(options.ConnectionName ?? "Default");
        }

        return CreateDbContext(options);
    }
}
