using System;

namespace Accounting.Common;

public class Store : StoreBase
{
    protected virtual AccountingDbContext Context { get; set; }

    public Store(AccountingDbContext context)
    {
        Context = context;
    }
}
