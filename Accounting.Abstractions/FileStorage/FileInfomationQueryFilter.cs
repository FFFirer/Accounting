using System;

using Accounting.Data;

namespace Accounting.FileStorage;

public class FileInfomationQueryFilter
{
    public string? FileId { get; set; }
    public string? FileName { get; set; }
    public string? BucketName { get; set; }

    public bool IncludeBucket { get; set; } = false;
}
