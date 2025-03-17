using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Books;

public class LedgerRecordAttachmentEntityConfiguration : IEntityTypeConfiguration<LedgerRecordAttachment>
{
    public void Configure(EntityTypeBuilder<LedgerRecordAttachment> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
