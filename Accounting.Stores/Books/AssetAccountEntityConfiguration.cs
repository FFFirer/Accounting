using System;

using Accounting.Asset;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Books;

public class AssetAccountEntityConfiguration : IEntityTypeConfiguration<AssetAccount>
{
    public void Configure(EntityTypeBuilder<AssetAccount> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type).HasMaxLength(100).IsRequired();
    }
}
