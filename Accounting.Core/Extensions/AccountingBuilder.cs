using System;
using System.Reflection;

using Accounting.Common;
using Accounting.FileStorage;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

public class AccountingBuilder
{
    public IServiceCollection Services { get; }

    public AccountingBuilder(IServiceCollection services)
    {
        Services = services;
    }
}

public static class AccountingBuilderExtensions
{
    public static AccountingBuilder AddAccountingCore(this IServiceCollection services)
    {
        return new AccountingBuilder(services);
    }

    public static AccountingBuilder AddFileStorage(this AccountingBuilder builder, Action<FileStorageOptions>? configureOptions = null)
    {
        // builder.Services.AddTransient<IConfigureOptions<FileStorageOptions>, ConfigureFileStorageOptions>(sp =>
        // {
        //     return new ConfigureFileStorageOptions(sp.GetRequiredService<IConfiguration>(), configureOptions);
        // });

        builder.Services.AddOptions<FileStorageOptions>()
            .Configure<IConfiguration>((options, configuration) =>
            {
                configuration.GetSection("FileStorage").Bind(options);
                configureOptions?.Invoke(options);
            });

        // 内部FileStorageProvider实现
        builder.Services.TryAddScoped<IFileStorageServiceFactory, FileStorageServiceFactory>();
        builder.Services.TryAddKeyedScoped<IFileStorageService, FileSystemFileStorageService>(FileStorageProvider.FileSystem);
        builder.Services.AddScoped<IFileStorageService>(sp => sp.GetRequiredService<IFileStorageServiceFactory>().GetService());

        return builder;
    }


    public static AccountingBuilder RegisterFileStorageProviders(this AccountingBuilder builder, params IEnumerable<Assembly> additionalAssemblies)
    {
        builder.Services.AddInjectionFromAssemblies(() => additionalAssemblies ?? [])
                .AddInjection(
                    (ass, t) =>
                    {
                        if (t.IsAbstract)
                        {
                            return false;
                        }

                        var fileStorageInterface = t.GetInterface(nameof(IFileStorageService));
                        if (fileStorageInterface is null)
                        {
                            return false;
                        }

                        return true;
                    },
                    (ass, t) =>
                    {
                        var provider = t.GetCustomAttribute<FileStorageProviderAttribute>();
                        if (provider is not null)
                        {
                            builder.Services.TryAddKeyedScoped(t, provider.Provider);
                        }
                    })
                .Register();

        return builder;
    }
}
