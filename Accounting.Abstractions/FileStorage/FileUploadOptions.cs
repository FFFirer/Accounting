using System;

namespace Accounting.FileStorage;

public class FileUploadOptions
{
    public bool DeleteWhenExpired { get; set; } = true;
    public TimeSpan? Expiration { get; set; } = TimeSpan.FromDays(1);
}
