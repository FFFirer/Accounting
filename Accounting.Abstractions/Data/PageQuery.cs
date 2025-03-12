using Accounting.Data;

namespace Accounting.Data;

public class PageQuery : IPageQuery
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}