using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Collections.Generic;

public static class CollectionExtensions
{
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)]this IEnumerable<T>? source)
    {
        return source is null || source.Any() == false;
    }
}
