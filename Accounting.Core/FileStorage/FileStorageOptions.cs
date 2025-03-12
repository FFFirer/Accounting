using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Accounting.FileStorage;

public class FileStorageOptions
{
    public string? Provider { get; set; }

    /// <summary>
    /// 文件系统 - 存储时的物理路径
    /// </summary>
    public string? FileSystemPhysicalPath { get; set; }

    /// <summary>
    /// 文件系统 - 生成上传令牌的密钥
    /// </summary>
    public string? FileSystemSecret { get; set; }
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
