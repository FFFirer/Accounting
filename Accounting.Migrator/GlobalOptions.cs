using System.CommandLine;

namespace Accounting.Migrator
{
    public class GlobalOptions
    {
        public static Option<string> ConnectionNameOption = new Option<string>("--connection-name");
    }
}