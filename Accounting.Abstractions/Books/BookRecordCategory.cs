using System;

namespace Accounting.Books;

public class BookRecordCategory
{
    public BookRecordCategory(Guid bookId, string name)
    {
        BookId = bookId;
        Name = name;
    }

    public long Id { get; set; }
    public Guid BookId { get; set; }

    public string Name { get; set; }
    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastModifiedTime { get; set; }
}
