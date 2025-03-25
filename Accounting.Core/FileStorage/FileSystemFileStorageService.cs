using System;
using System.Security.Claims;

using Accounting.Data;

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Accounting.FileStorage;


[FileStorageProvider(FileStorageProvider.FileSystem)]
public class FileSystemFileStorageService : IFileStorageService, IFileUploadService
{
    static FileStorageOptionsFileSystemValidation validtion = new FileStorageOptionsFileSystemValidation();

    protected virtual IOptions<FileStorageOptions> Options { get; set; }

    protected virtual ErrorDescriptions Errors { get; set; }

    protected virtual IFileStorageStore FileStorageStore { get; set; }
    protected virtual ILogger _logger { get; set; }

    protected IHostEnvironment _environment { get; set; }

    public FileSystemFileStorageService(
        IFileStorageStore fileStorageStore,
        IOptions<FileStorageOptions> options,
        IHostEnvironment environment,
        ILogger<FileSystemFileStorageService> logger,
        ErrorDescriptions? descriptions = null)
    {
        this.FileStorageStore = fileStorageStore;
        this.Options = options;
        this.Errors = descriptions ?? new ErrorDescriptions();
        this._environment = environment;
        this._logger = logger;
    }

    public Task<string> GetUploadTokenAsync(string? bucketName = "Default", CancellationToken cancellationToken = default)
    {
        Validate();

        var fileId = Guid.NewGuid().ToString();
        List<Claim> claims = [new Claim(TokenDefaults.UploadFileId, fileId)];
        if (string.IsNullOrWhiteSpace(bucketName) == false)
        {
            claims.Add(new Claim(TokenDefaults.BucketName, bucketName));
        }

        var token = JwtUtils.GenerateToken(
            Options.Value.FileSystemSecret,
            TokenDefaults.Issuer,
            TokenDefaults.Audience,
            Options.Value.UploadTokenExpirationMinutes,
            claims);

        return Task.FromResult(token);
    }

    private void Validate()
    {
        var result = validtion.Validate(Options.Value);

        if (result.IsValid)
        {
            return;
        }

        throw new InvalidOperationException($"未配置正确文件存储服务选项: {string.Join(";", result.Errors.Select(e => e.ErrorMessage))}");
    }

    public Task<Result<ClaimsPrincipal>> ValidateTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var claimsPrincipal = JwtUtils.ValidateToken(token, Options.Value.FileSystemSecret, TokenDefaults.Issuer, TokenDefaults.Audience);

        if (claimsPrincipal is null)
        {
            return Task.FromResult(Result<ClaimsPrincipal>.Failed(Errors.InvalidUploadToken()));
        }

        return Task.FromResult(Result.Success(claimsPrincipal));
    }

    public async Task<Result> UploadAsync(string uploadToken, string originalFileName, IBrowserFile uploadFile, Action<FileUploadOptions>? configureUploadOptions = null, CancellationToken cancellationToken = default)
    {
        return await UploadAndGetAsync(uploadToken, originalFileName, uploadFile, configureUploadOptions, cancellationToken);
    }

    private string ResolveSaveDirectory(string path)
    {
        var directory = Path.IsPathRooted(path)
        ? Path.Combine(path)
        : Path.Combine(AppContext.BaseDirectory, path);

        if (Directory.Exists(directory) == false)
        {
            directory = Directory.CreateDirectory(directory).FullName;

            _logger.LogInformation("Created! FileStorageDirectory: {Path}", directory);
        }

        return directory;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var file = await FileStorageStore.FindByIdAsync(id, cancellationToken);

        if (file is null || file.Deleted)
        {
            return;
        }

        file.Deleted = true;

        await FileStorageStore.UpdateAsync(file, cancellationToken);

        // TODO: EmitEvent FileDeleted

        if (file.DeleteWhenExpired)
        {
            if (File.Exists(file.StoragePath))
            {
                File.Delete(file.StoragePath);
            }
        }
    }

    private Task UpdateSavedFileInfo(FileInformation fileInfo)
    {
        if (File.Exists(fileInfo.StoragePath) == false)
        {
            throw new InvalidOperationException("保存文件失败");
        }

        fileInfo.Size = new FileInfo(fileInfo.StoragePath).Length;
        fileInfo.UploadTime = DateTimeOffset.UtcNow;

        return Task.CompletedTask;
    }

    private void MergeUploadOptions(FileInformation fileInfo, FileUploadOptions upload)
    {
        fileInfo.DeleteWhenExpired = upload.DeleteWhenExpired;

        if (upload.Expiration is null)
        {
            fileInfo.ExpirationTime = null;
        }
        else
        {
            fileInfo.ExpirationTime = fileInfo.CreatedTime + upload.Expiration;
        }
    }

    public async Task UpdateAsync(string id, Action<FileUploadOptions> configure, CancellationToken cancellationToken = default)
    {
        var fileId = Guid.Parse(id);

        var file = await FileStorageStore.FindByIdAsync(fileId, cancellationToken);

        if (file is null || file.Deleted || file.ExpirationTime < DateTimeOffset.UtcNow)
        {
            throw new InvalidOperationException("无法修改文件信息");
        }

        var options = new FileUploadOptions();
        configure?.Invoke(options);

        MergeUploadOptions(file, options);

        await FileStorageStore.UpdateAsync(file, cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var fileId = Guid.Parse(id);

        await DeleteAsync(fileId, cancellationToken);
    }

    public async Task<Result<FileInformation>> UploadAndGetAsync(string uploadToken, string originalFileName, IBrowserFile uploadFile, Action<FileUploadOptions>? configureUploadOptions = null, CancellationToken cancellationToken = default)
    {
        Validate();

        var result = await ValidateTokenAsync(uploadToken);

        if (result.Succeeded == false) { return Result<FileInformation>.Failed(result.Errors); }

        var fileId = result.Data.FindFirstValue(TokenDefaults.UploadFileId);
        var bucketName = result.Data.FindFirstValue(TokenDefaults.BucketName);

        if (string.IsNullOrWhiteSpace(fileId))
        {
            return Result<FileInformation>.Failed(Errors.NotExistsUploadFileId());
        }

        var saveDirectory = ResolveSaveDirectory(Options.Value.FileSystemPhysicalPath);
        var tempFileName = Path.GetRandomFileName();

        var upload = new FileUploadOptions();
        configureUploadOptions?.Invoke(upload);

        var fileInfo = new FileInformation
        {
            Id = Guid.Parse(fileId!),
            OriginalFileName = originalFileName,
            CreatedTime = DateTimeOffset.UtcNow,
            StorageProvider = Options.Value.Provider!,
            StoragePath = Path.Combine(saveDirectory, tempFileName),
        };

        MergeUploadOptions(fileInfo, upload);

        using (var fs = new FileStream(fileInfo.StoragePath, FileMode.Create))
        {
            await uploadFile.OpenReadStream(maxAllowedSize: Options.Value.MaxFileSize).CopyToAsync(fs, cancellationToken);
        }

        if (string.IsNullOrWhiteSpace(bucketName) == false)
        {
            var bucket = await FileStorageStore.GetOrCreateBucketAsync(bucketName, cancellationToken);
            fileInfo.Bucket = bucket;
        }

        await FileStorageStore.CreateAsync(fileInfo, cancellationToken);
        await UpdateSavedFileInfo(fileInfo);

        return Result.Success(fileInfo);
    }

    private class TokenDefaults
    {
        public const string UploadFileId = nameof(UploadFileId);
        public const string BucketName = nameof(BucketName);


        public const string Issuer = "Accounting";
        public const string Audience = "Accounting";
    }
}
