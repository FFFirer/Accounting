    using System;
using System.ComponentModel.DataAnnotations;

namespace Accounting.FileStorage;

public class FileInformation
{
    [Key]
    public Guid Id { get; set; } 

    [MaxLength(50)]
    public string StorageProvider { get; set; } = "FileSystem";
    public string? StoragePath { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    public List<string> Tags { get; set; } = [];

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }

    public string? OriginalFileName { get; set; }

    public long? Size { get; set; }

    /// <summary>
    /// 上传时间
    /// </summary>
    public DateTimeOffset UploadTime { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTimeOffset? ExpirationTime { get; set; }

    public bool Deleted { get; set; }

    /// <summary>
    /// 过期时删除
    /// </summary>
    public bool DeleteWhenExpired { get; set; }

    /// <summary>
    /// 存储桶
    /// </summary>
    public StorageBucket? Bucket { get; set; }
}
