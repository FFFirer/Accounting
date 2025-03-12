using System;

namespace Accounting.Data;

public interface IPageQuery
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
