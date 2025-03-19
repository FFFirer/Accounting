using System;
using System.Collections.Generic;
using System.Text;

using Accounting.Quartz;
using Accounting.Quartz.Migrations;

namespace Accounting.Migrator;

public static class Constants
{
    public static class DbContextTokens
    {
        public const string Default = "default";
        public const string Quartz = "quartz";
    }

    public static IReadOnlyDictionary<string, Type> DbContexts = new Dictionary<string, Type>()
    {
        [DbContextTokens.Default] = typeof(AccountingDbContext),
        [DbContextTokens.Quartz] = typeof(AccountingQuartzDbContext),
    };

    public static IReadOnlyDictionary<string, Type> DbContextFactories = new Dictionary<string, Type>()
    {
        [DbContextTokens.Default] = typeof(DesignTimeDbContextFactory),
        [DbContextTokens.Quartz] = typeof(AccountingQuartzDbContextFactory),
    };
}
