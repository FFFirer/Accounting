using System;

namespace Accounting.Data;

public class PageList<T>
{
    public PageList(int total) : this(total, [])
    {

    }

    public PageList(int total, List<T> datas)
    {
        TotalCount = total;
        Datas = datas;
    }

    public int TotalCount { get; set; }
    public List<T> Datas { get; set; }
}
