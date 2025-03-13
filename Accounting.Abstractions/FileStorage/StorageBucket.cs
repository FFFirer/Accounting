using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting.FileStorage;

public class StorageBucket
{
    public StorageBucket(string name)
    {
        this.Name = name;
        this.CreatationTime = DateTimeOffset.UtcNow;
    }

    [MaxLength(5000)]
    public string Name { get; set; }
    public DateTimeOffset CreatationTime { get; set; }

    public List<FileInformation> Files { get; set; } = [];
}
