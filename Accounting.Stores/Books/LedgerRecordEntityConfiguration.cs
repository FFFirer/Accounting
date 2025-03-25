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
        builder.HasKey(x => new { x.SourceChannelCode, x.SourceChannelId });

        builder.Property(x => x.FlowDirection)
            .HasConversion(
               v => v == null ? null : v.ToString(),
               s => string.IsNullOrWhiteSpace(s) ? null : (AssetFlowDirection)Enum.Parse(typeof(AssetFlowDirection), s));

        builder.Property(x => x.Tags).HasColumnType("text[]");
    }
}
