using System;
using System.Collections.Generic;
using System.Text;

using Accounting.Documents;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var documentEntity = builder.Entity<Document>();

            builder.ApplyConfiguration(new UserRefreshTokenConfiguration());
            builder.ApplyConfiguration(new DocumentEntityConfiguration());

            builder.ApplyConfiguration(new JsonDocumentEntityConfiguration<SiteSettings>(documentEntity));
        }
    }
}
