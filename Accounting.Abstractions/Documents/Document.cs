using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accounting.Documents;

public class Document<T>
{
    public Document() : this(default)
    {

    }

    public Document(T? content)
    {
        Id = 0;
        Type = DocumentHelper.GetTypeFullName(typeof(T));
        Version = 0;
        LastModified = DateTimeOffset.UtcNow;
    }

    [Column("Id")]
    public long Id { get; set; }

    [Column("Type")]
    public string Type { get; set; } = "";

    [Column("Content")]
    public T? Content { get; set; }

    [Column("Version")]
    public long Version { get; set; }

    [Column("LastModified")]
    public DateTimeOffset LastModified { get; set; }
}

public class Document : Document<string>
{

}
