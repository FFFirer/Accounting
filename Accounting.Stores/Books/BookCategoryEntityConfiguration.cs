using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Books;

// public class BookCategoryEntityConfiguration : IEntityTypeConfiguration<BookRecordCategory>
// {
//     public void Configure(EntityTypeBuilder<BookRecordCategory> builder)
//     {
//         builder.HasKey(x => x.Id);

//         builder.HasIndex(x => new { x.BookId, x.Name }).IsUnique();
//     }
// }
