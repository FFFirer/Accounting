using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Books;

public class LedgerCategoryEntityConfiguration : IEntityTypeConfiguration<LedgerCategory>
{
    public void Configure(EntityTypeBuilder<LedgerCategory> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
