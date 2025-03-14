using System;
using System.Linq.Expressions;
using System.Security.Claims;

using Accounting.Data;

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;

namespace Accounting.FileStorage;

public interface IFileUploadService
{
    Task<string> GetUploadTokenAsync(string? bucketName = "Default", CancellationToken cancellationToken = default);
    Task<Result<ClaimsPrincipal>> ValidateTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<Result> UploadAsync(string uploadToken, string originalFileName, IBrowserFile uploadFile, Action<FileUploadOptions>? configureUploadOptions = null, CancellationToken cancellationToken = default);
}

public interface IFileStorageService
{
    Task UpdateAsync(string id, Action<FileUploadOptions> options, CancellationToken cancellationToken);
    // Task DeleteAsync(string id, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

}

