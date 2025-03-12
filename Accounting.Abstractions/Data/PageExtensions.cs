using System;

namespace Accounting.Data;

public static class PageExtensions
{
    public static int GetSkipCount(this IPageQuery pageQuery)
    {
        var count = (pageQuery.PageIndex - 1) * pageQuery.PageSize;

        return count < 0 ? 0 : count;
    }
}
