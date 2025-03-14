using System;

namespace Accounting.Data;

public static class PageExtensions
{
    public static IQueryable<T> DoPage<T>(this IQueryable<T> source, IPageQuery page)
    {
        return source.Skip(page.GetSkipCount()).Take(page.PageSize);
    }
}
