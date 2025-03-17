using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Books;

public class LedgerRecordEntityConfiguration : IEntityTypeConfiguration<LedgerRecord>
{
    public void Configure(EntityTypeBuilder<LedgerRecord> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
