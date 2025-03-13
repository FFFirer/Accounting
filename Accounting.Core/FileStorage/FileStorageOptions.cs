using System;

using FluentValidation;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Accounting.FileStorage;

public class FileStorageOptions
{
    public string? Provider { get; set; } = FileStorageProvider.FileSystem;

    /// <summary>
    /// 文件系统 - 存储时的物理路径
    /// </summary>
    public string FileSystemPhysicalPath { get; set; } = ".\\App_Data\\Files";

    /// <summary>
    /// 文件系统 - 生成上传令牌的密钥
    /// </summary>
    public string FileSystemSecret { get; set; } = "1wefghjkdrtyuijbvdrt678ikjhgfdr6";

    public int UploadTokenExpirationMinutes { get; set; } = 30;

    public long MaxFileSize { get; set; } = 536_870_912;
}

public class FileStorageOptionsFileSystemValidation : AbstractValidator<FileStorageOptions>
{
    public FileStorageOptionsFileSystemValidation()
    {
        RuleFor(x => x.FileSystemPhysicalPath).NotNull().NotEmpty();
        RuleFor(x => x.FileSystemSecret).NotNull().NotEmpty();
    }
}

public class ConfigureFileStorageOptions : IConfigureOptions<FileStorageOptions>
{
    protected virtual string SectionName => "FileStorage";
    protected virtual IConfiguration Configuration { get; }
    protected virtual Action<FileStorageOptions>? ConfigureOptions { get; }

    public ConfigureFileStorageOptions(IConfiguration configuration, Action<FileStorageOptions>? configureOptions = null)
    {
        Configuration = configuration;
        ConfigureOptions = configureOptions;
    }

    public void Configure(FileStorageOptions options)
    {
        Configuration.GetSection(SectionName).Bind(options);

        this.ConfigureOptions?.Invoke(options);
    }
}
