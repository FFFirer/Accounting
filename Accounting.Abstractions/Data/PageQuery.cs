using Accounting.Data;

namespace Accounting.Data;

public class PageQuery : IPageQuery
{
    public PageQuery(int index, int size) {
        PageIndex = index;
        PageSize = size;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}