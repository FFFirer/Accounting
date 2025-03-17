using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Books;

public class LedgerTagEntityConfiguration : IEntityTypeConfiguration<LedgerTag>
{
    public void Configure(EntityTypeBuilder<LedgerTag> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
