using CommandLine;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Accounting
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AccountingDbContext>
    {
        public class FactoryOptions
        {
            [Option("--connection-name")]
            public string ConnectionName { get; set; } = "Default";

            [Option("--connection-string")]
            public string? ConnectionString { get; set; }
        }

        public DesignTimeDbContextFactory()
        {

        }

        private IConfiguration? Configuration { get; set; }
        public DesignTimeDbContextFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration BuildConfiguration()
        {
            if(Configuration is null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                configurationBuilder.AddUserSecrets<DesignTimeDbContextFactory>();
                configurationBuilder.AddEnvironmentVariables(prefix: "Accounting_");

                Configuration = configurationBuilder.Build();
            }

            return Configuration;
        }

        public AccountingDbContext CreateDbContext(string[] args)
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

        public AccountingDbContext CreateDbContext(FactoryOptions factoryOptions)
        {

            var contextOptionsBuilder = new DbContextOptionsBuilder<AccountingDbContext>();
            contextOptionsBuilder.UseNpgsql(
                factoryOptions.ConnectionString,
                options => options.MigrationsAssembly(this.GetType().Assembly));

            var contextOptions = contextOptionsBuilder.Options;
            var context = new AccountingDbContext(contextOptions);

            return context;
        }
    }
}