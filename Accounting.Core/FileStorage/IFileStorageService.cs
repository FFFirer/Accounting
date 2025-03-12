using System;

namespace Accounting.FileStorage;

public interface IFileStorageService
{
}

[FileStorageProvider(FileStorageProvider.FileSystem)]
public class FileSystemFileStorageService : IFileStorageService {
    
}
