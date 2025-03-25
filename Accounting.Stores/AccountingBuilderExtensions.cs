using Accounting;
using Accounting.FileStorage;
using Accounting.Imports;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class AccountingBuilderExtensions
{
    public static AccountingBuilder AddEntityFrameworkCoreStores(this AccountingBuilder builder) {
        builder.Services.AddScoped<IFileStorageStore, FileStorageStore>();
        builder.Services.AddScoped<IImportRecordStore, ImportRecordStore>();

        return builder;
    }
}