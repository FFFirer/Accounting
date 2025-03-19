using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Migrator;

public static class Helper
{

    public static IConfiguration BuildConfiguration()
    {

        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        configurationBuilder.AddUserSecrets<Root>();
        configurationBuilder.AddEnvironmentVariables(prefix: "Accounting_");

        var configuration = configurationBuilder.Build();

        return configuration;
    }

    public static DbContext BuildDbContext(string? connectionName)
    {
        var configuration = BuildConfiguration();

        var factory = new DesignTimeDbContextFactory(configuration);

        return factory.CreateDbContext(new DesignTimeDbContextFactory.FactoryOptions
        {
            ConnectionString = configuration.GetConnectionString(connectionName ?? "Default")
        });
    }

    public static DbContext? CreateDbContextFromFactory(Type factoryType, string? connectionName)
    {
        var configuration = BuildConfiguration();
        dynamic? factory = Activator.CreateInstance(factoryType, configuration);

        return factory?.CreateDbContext(new string[]
        {
            "connection-name",
            (connectionName ?? "Default")
        });
    }

    public static IServiceProvider BuildServiceProvider(string connectionName)
    {
        var configuration = BuildConfiguration();
        
        var services = new ServiceCollection();

        services.AddLogging();
        services.AddDataProtection();
        services.AddDbContext<AccountingDbContext>(options => options.UseNpgsql(configuration.GetConnectionString(connectionName)));
        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<AccountingDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();
        
        return services.BuildServiceProvider();
    }
}