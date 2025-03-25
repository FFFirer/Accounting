using System.Text.Json.Nodes;
using Accounting.Imports;
using Accounting.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting;

public class ImportRecordConfiguration : IEntityTypeConfiguration<ImportRecord>
{
    public void Configure(EntityTypeBuilder<ImportRecord> builder)
    {
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.Channel);
        builder.Property(x => x.Status).HasConversion<EnumStringValueConverter<ImportStatus>>();
    }
}
