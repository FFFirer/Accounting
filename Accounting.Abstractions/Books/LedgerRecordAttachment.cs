using System;

using Accounting.FileStorage;

namespace Accounting.Books;

public class LedgerRecordAttachment
{
    public LedgerRecordAttachment(FileInformation file) {
        File = file;
        CreatedTime = DateTimeOffset.UtcNow;
        LastModifiedTime = DateTimeOffset.UtcNow;
    }

    public long Id {get;set;}

    public FileInformation File { get; set; }

    public DateTimeOffset CreatedTime { get; set; }
    public DateTimeOffset LastModifiedTime { get; set; }
}
