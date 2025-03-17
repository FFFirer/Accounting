using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Books;

public class BookTagEntityConfiguration : IEntityTypeConfiguration<BookRecordTag>
{
    public void Configure(EntityTypeBuilder<BookRecordTag> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.BookId, x.Name }).IsUnique();
    }
}
