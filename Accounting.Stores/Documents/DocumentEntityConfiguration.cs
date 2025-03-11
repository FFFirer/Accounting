using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Documents;

public class DocumentEntityConfiguration : IEntityTypeConfiguration<Document>
{
    protected virtual string TableName { get; } = "Documents";

    public virtual void Configure(EntityTypeBuilder<Document> builder)
    {
        ConfigureTableName(builder);
        ConfigureProperties(builder);
    }

    protected virtual void ConfigureProperties(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable(TableName);
    }

    protected virtual void ConfigureTableName(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Version)
            .IsRequired()
            .HasDefaultValue(0)
            .IsRowVersion();
    }
}
