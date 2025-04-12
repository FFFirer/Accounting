using System;
using Accounting.Books;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Web.Components.Books.Pages;

public partial class AllLedgerRecords : ComponentBase
{
    [Inject]
    private AccountingDbContext DbContext { get; set; } = default!;
    private PaginationState paginationState = new PaginationState { ItemsPerPage = 50 };

    private Filter Input { get; set; } = new();
    private string GetRowClass(LedgerRecord record) => "hover:bg-base-300";


    public async ValueTask<GridItemsProviderResult<LedgerRecord>> GetRecords(GridItemsProviderRequest<LedgerRecord> request)
    {
        var query = this.DbContext.LedgerRecords.AsNoTracking();

        var totalCount = await query.CountAsync(request.CancellationToken);

        var pageQuery = query.OrderByDescending(x => x.PayTime).Page(paginationState.CurrentPageIndex, paginationState.ItemsPerPage);

        var records = await pageQuery.ToListAsync(request.CancellationToken);

        return new GridItemsProviderResult<LedgerRecord>
        {
            Items = records,
            TotalItemCount = totalCount
        };
    }

    public class Filter
    {
        public string? ChannelCode { get; set; }
    }
}
