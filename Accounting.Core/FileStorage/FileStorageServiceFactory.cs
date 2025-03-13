using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Accounting.FileStorage;

public sealed class FileStorageServiceFactory : IFileStorageServiceFactory
{
    private readonly ILogger _logger;

    private IOptionsMonitor<FileStorageOptions> Options;
    private IServiceProvider ServiceProvider;

    static FileStorageOptionsFileSystemValidation validation = new FileStorageOptionsFileSystemValidation();

    public FileStorageServiceFactory(
        IOptionsMonitor<FileStorageOptions> options, 
        IServiceProvider serviceProvider,
        ILogger<FileStorageServiceFactory> logger)
    {
        Options = options;
        ServiceProvider = serviceProvider;
       
        _logger = logger;
    }

    public IFileStorageService GetService()
    {
        Validate();

        return ServiceProvider.GetRequiredKeyedService<IFileStorageService>(Options.CurrentValue.Provider);
    }

    public IFileUploadService GetUploadService()
    {
        Validate();

        return ServiceProvider.GetRequiredKeyedService<IFileUploadService>(Options.CurrentValue.Provider);
    }

    private void Validate()
    {
        _logger.LogDebug("FileStorageOptions: {@Value}", Options.CurrentValue);

        var result = validation.Validate(Options.CurrentValue);

        if(result.IsValid == false)
        {
            throw new ArgumentException(string.Join(",", result.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
