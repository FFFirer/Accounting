using System;

using Accounting.Asset;
using Accounting.Data;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Web.Components.AssetAccounts.Pages;

public partial class Index : ComponentBase
{
    [Inject]
    private AccountingDbContext AccountingDb { get; set; } = default!;
    public async Task UpdateFilter()
    {
        await pagination.SetCurrentPageIndexAsync(1);
    }

    protected override async Task OnParametersSetAsync()
    {
        await pagination.SetCurrentPageIndexAsync(1);
    }
    private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
    private string GetRowClass(AssetAccount _) => "hover:bg-base-300";

    public async ValueTask<GridItemsProviderResult<AssetAccount>> GetAssetAccounts(GridItemsProviderRequest<AssetAccount> request)
    {
        var query = AccountingDb.AssetAccounts.AsNoTracking();

        var totalCount = await query.CountAsync();

        query = query.DoPage(new PageQuery(pagination.CurrentPageIndex, pagination.ItemsPerPage));

        var items = await query.ToListAsync();

        var result = new GridItemsProviderResult<AssetAccount>
        {
            TotalItemCount = totalCount,
            Items = items
        };

        return result;
    }
}
