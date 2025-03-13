using System;

namespace Accounting.FileStorage;

public interface IFileStorageServiceFactory
{
    IFileStorageService GetService();
    IFileUploadService GetUploadService();
}
