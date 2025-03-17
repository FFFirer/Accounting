using System;

using Accounting.Asset;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Accounting.Books;

public class LedgerRecordEntityConfiguration : IEntityTypeConfiguration<LedgerRecord>
{
    public void Configure(EntityTypeBuilder<LedgerRecord> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FlowDirection)
            .HasConversion(
               v => v.ToString(),
               s => (AssetFlowDirection)Enum.Parse(typeof(AssetFlowDirection), s));

        builder.HasMany(x => x.Tags).WithMany();
    }
}
