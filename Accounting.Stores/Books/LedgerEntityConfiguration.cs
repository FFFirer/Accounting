using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Books;

public class LedgerEntityConfiguration : IEntityTypeConfiguration<Ledger>
{
    public void Configure(EntityTypeBuilder<Ledger> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Categories).WithOne().HasForeignKey(x => x.LedgerId).IsRequired();
        builder.HasMany(x => x.Tags).WithOne().HasForeignKey(x => x.LedgerId).IsRequired();
    }
}
