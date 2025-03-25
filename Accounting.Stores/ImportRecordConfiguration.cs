using System.Text.Json.Nodes;
using Accounting.Imports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting;

public class ImportRecordConfiguration : IEntityTypeConfiguration<ImportRecord>
{
    public void Configure(EntityTypeBuilder<ImportRecord> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.ImportId);
        builder.OwnsOne(x => x.Channel);
    }
}

public class ImportRecordItemConfiguration : IEntityTypeConfiguration<ImportRecordItem>
{
    public void Configure(EntityTypeBuilder<ImportRecordItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Data).HasConversion(GenericJsonValueConverter<JsonObject>.Default());
    }
}