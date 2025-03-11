using System;

namespace Accounting.Documents;

public static class DocumentHelper
{
    public static string GetTypeFullName(this Type type) => type.FullName!;
    public static string GetTypeAssemblyQualifiedName(this Type type) => type.AssemblyQualifiedName!;
}
