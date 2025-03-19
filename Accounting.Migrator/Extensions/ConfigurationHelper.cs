using Microsoft.Extensions.Configuration;

namespace Accounting.Migrator
{
    public class ConfigurationHelper
    {
        public static IConfiguration BuildConfiguration()
        {    
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationBuilder.AddEnvironmentVariables(prefix: "Accounting_");
            
            var configuration = configurationBuilder.Build();
            
            return configuration; 
        }
        
        public static string? GetConnectionString(string connectionName)
        {
            return BuildConfiguration().GetConnectionString(connectionName);
        }
    }
}