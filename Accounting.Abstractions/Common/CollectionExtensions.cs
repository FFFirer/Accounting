using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Collections.Generic;

public static class CollectionExtensions
{
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)]this IEnumerable<T>? source)
    {
        return source is null || source.Any() == false;
    }

    public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int page, int size)
    {
        var skip = (page - 1) * size;
        if(skip < 0) { skip = 0; }

        return source.Skip(skip).Take(size);
    }
}
