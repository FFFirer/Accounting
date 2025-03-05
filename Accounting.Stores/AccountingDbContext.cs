using System;
using System.Collections.Generic;
using System.Text;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserRefreshTokenConfiguration());
        }
    }
}
