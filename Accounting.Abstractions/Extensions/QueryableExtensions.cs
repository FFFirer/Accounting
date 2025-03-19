using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting;

public static class QueryableExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> source, int page, int size)
    {
        var skip = (page - 1)*size;
        if(skip < 0) { skip = 0; }

        return source.Skip(skip).Take(size);
    } 
}
