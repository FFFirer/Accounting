using System;
using System.Collections.Generic;
using System.Text;

using Accounting.Asset;
using Accounting.Books;
using Accounting.Documents;
using Accounting.FileStorage;
using Accounting.ValueConverters;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Accounting
{
    public class AccountingDbContext : IdentityDbContext<User>
    {
        public AccountingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserRefreshToken> UserRefreshTokens => Set<UserRefreshToken>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Document<SiteSettings>> SiteSettings => Set<Document<SiteSettings>>();
        public DbSet<FileInformation> FileInformations => Set<FileInformation>();
        public DbSet<StorageBucket> StorageBuckets => Set<StorageBucket>();

        public DbSet<AssetAccount> AssetAccounts => Set<AssetAccount>();
        public DbSet<Ledger> Ledgers => Set<Ledger>();
        public DbSet<LedgerRecord> LedgerRecords => Set<LedgerRecord>();
        public DbSet<LedgerCategory> LedgerCategories => Set<LedgerCategory>();
        public DbSet<LedgerTag> LedgerTags => Set<LedgerTag>();
        public DbSet<LedgerRecordAttachment> LedgerRecordAttachments => Set<LedgerRecordAttachment>();

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            // 自动处理时区转换
            configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeUtcConverter>();
            configurationBuilder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetUtcConverter>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            var documentEntity = builder.Entity<Document>();

            builder.ApplyConfiguration(new UserRefreshTokenConfiguration());
            builder.ApplyConfiguration(new DocumentEntityConfiguration());

            builder.ApplyConfiguration(new JsonDocumentEntityConfiguration<SiteSettings>(documentEntity));

            builder.ApplyConfigurationsFromAssembly(typeof(AccountingDbContext).Assembly);

            builder.Entity<FileInformation>().HasOne(f => f.Bucket);
        }
    }
}
