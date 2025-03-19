using System.CommandLine;
using System.CommandLine.Invocation;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace Accounting.Migrator
{
    public class ContextsCommand : Command
    {
        public ContextsCommand() : base("contexts")
        {
            this.AddCommand(new ListCommand());
        }

        public class ListCommand : Command
        {
            public ListCommand() : base("list")
            {
                this.SetHandler(ListContexts);
            }

            private void ListContexts(InvocationContext context)
            {
                var logger = context.GetLoggerFactory().CreateLogger("Contexts.List");

                var format = context.ParseResult.GetValueForOption(GlobalOptions.FormatOption) ?? "table";

                var contexts = Constants.DbContexts
                    .Select(x => new
                    {
                        Name = x.Key,
                        DbContext = x.Value.FullName
                    })
                    .ToList();

                logger?.LogDebug("Format: {Option}", format);

                context.FormatToOutput(contexts, format);
            }
        }
    }
}
