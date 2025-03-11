using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Documents;

public class JsonDocumentEntityConfiguration<TContent> : IEntityTypeConfiguration<Document<TContent>> where TContent : class
{
    protected virtual string TableName { get; } = "Documents";
    protected virtual EntityTypeBuilder<Document> DocumentBuilder { get; set; }

    public JsonDocumentEntityConfiguration(EntityTypeBuilder<Document> documentBuilder)
    {
        DocumentBuilder = documentBuilder;
    }

    public void Configure(EntityTypeBuilder<Document<TContent>> builder)
    {
        ConfigurePrimaryKeyConvention(builder);
        ConfigureTableNameConvention(builder);
        ConfigurePropertyConvention(builder);
        ConfigureQueryFilter(builder);
        ConfigureDocumentRelational(builder);
    }

    private void ConfigureDocumentRelational(EntityTypeBuilder<Document<TContent>> builder)
    {
        DocumentBuilder
            .HasOne<Document<TContent>>()
            .WithOne()
            .HasForeignKey<Document<TContent>>(x => x.Id);
    }

    private void ConfigureQueryFilter(EntityTypeBuilder<Document<TContent>> builder)
    {
        builder.HasQueryFilter(x => x.Type == typeof(TContent).GetTypeFullName());
    }

    protected virtual void ConfigurePrimaryKeyConvention(EntityTypeBuilder<Document<TContent>> builder)
    {
        builder.HasKey(x => x.Id);
    }

    protected virtual void ConfigurePropertyConvention(EntityTypeBuilder<Document<TContent>> builder)
    {
        builder.Property(x => x.Content)
            .HasColumnName("Content")
            .HasConversion(GenericJsonValueConverter<TContent>.Default());

        builder.Property(x => x.Type)
            .HasColumnName("Type")
            .IsRequired();

        builder.Property(x => x.Version)
            .HasColumnName("Version")
            .IsRequired()
            .HasDefaultValue(0)
            .IsRowVersion();
    }

    private void ConfigureTableNameConvention(EntityTypeBuilder<Document<TContent>> builder)
    {
        builder.ToTable(TableName);
    }
}
