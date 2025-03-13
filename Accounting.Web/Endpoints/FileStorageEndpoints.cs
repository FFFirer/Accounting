using System;

using Accounting.Data;
using Accounting.FileStorage;

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Web.Endpoints;

public static class FileStorageEndpoints
{
    public static IEndpointRouteBuilder MapFileStorageEndpoints(this IEndpointRouteBuilder endpoints, string? prefix = null)
    {
        IEndpointRouteBuilder group = string.IsNullOrWhiteSpace(prefix) ? endpoints : endpoints.MapGroup(prefix);

        group.MapPut("/upload", Upload);
        group.MapGet("/preupload", PreUpload);

        return endpoints;
    }

    public static async Task<Ok<string>> PreUpload(
        [FromServices] IFileUploadService service,
        [FromServices] IHttpContextAccessor httpContextAccessor)
    {
        var token = await service.GetUploadTokenAsync(httpContextAccessor.HttpContext!.RequestAborted);
        var request = httpContextAccessor.HttpContext?.Request;
        var uploadUrl = $"{request?.Scheme}://{request?.Host}{request?.PathBase}/upload?token={token}";
        return TypedResults.Ok(uploadUrl);
    }

    public static async Task<Ok> Upload(
        [FromQuery] string token,
        [FromForm] IBrowserFile file,
        [FromServices] IFileUploadService service,
        [FromServices] IHttpContextAccessor httpContextAccessor)
    {
        var result = await service.UploadAsync(token, file.Name, file, null, httpContextAccessor.HttpContext!.RequestAborted);

        result.ThrowIfFailed();

        return TypedResults.Ok();
    }
}
