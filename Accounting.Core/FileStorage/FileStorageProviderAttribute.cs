using System;

namespace Accounting.FileStorage;


[System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
sealed class FileStorageProviderAttribute : System.Attribute
{
    // See the attribute guidelines at
    //  http://go.microsoft.com/fwlink/?LinkId=85236
    public string Provider { get; init; }

    // This is a positional argument
    public FileStorageProviderAttribute(string provider)
    {
        this.Provider = provider;
    }
}
