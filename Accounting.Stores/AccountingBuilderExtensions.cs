using Accounting;
using Accounting.FileStorage;

using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class AccountingBuilderExtensions
{
    public static AccountingBuilder AddEntityFrameworkCoreStores(this AccountingBuilder builder) {
        builder.Services.AddScoped<IFileStorageStore, FileStorageStore>();

        return builder;
    }
}