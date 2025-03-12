using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Accounting.FileStorage;

public sealed class FileStorageServiceFactory : IFileStorageServiceFactory
{
    private IOptionsMonitor<FileStorageOptions> Options;
    private IServiceProvider ServiceProvider;

    public FileStorageServiceFactory(IOptionsMonitor<FileStorageOptions> options, IServiceProvider serviceProvider)
    {
        Options = options;
        ServiceProvider = serviceProvider;
    }

    public IFileStorageService GetService()
    {
        return ServiceProvider.GetRequiredKeyedService<IFileStorageService>(Options.CurrentValue.Provider);
    }
}
