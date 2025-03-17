using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Books;

// public class BookEntityConfiguration : IEntityTypeConfiguration<BookEntity>
// {
//     public void Configure(EntityTypeBuilder<BookEntity> builder)
//     {
//         builder.HasKey(x => x.Id);

//         builder.HasIndex(x => x.Name).IsUnique();
//     }
// }
