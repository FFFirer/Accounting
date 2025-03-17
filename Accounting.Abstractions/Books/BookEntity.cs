namespace Accounting.Books;

public class BookEntity
{
    public BookEntity(string name) {
        Name = name;
        Id = Guid.NewGuid();
        CreatedTime = DateTimeOffset.UtcNow;
        LastModifiedTime = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<BookRecordCategory>? Categories { get; set; }
    public List<BookRecordTag>? Tags { get; set; }

    public int MonthStartAtDay { get; set; } = 1;

    public List<BookRecord>? BookRecords {get;set;}

    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastModifiedTime { get; set; }
}