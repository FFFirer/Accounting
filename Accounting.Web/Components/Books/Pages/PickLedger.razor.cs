using System;

using Accounting.Books;

using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Web.Components.Books.Pages;

public partial class PickLedger : ComponentBase
{
    [Inject]
    private AccountingDbContext AccountingDb { get; set; } = default!;

    [Parameter]
    public string? ContinuePath { get; set; }

    private List<Ledger>? Ledgers { get; set; }

    private string GetNavLinkUrl(Ledger ledger) => $"/Ledgers/{ledger.Id}/{ContinuePath}";

    protected override async Task OnInitializedAsync()
    {
        this.Ledgers = await AccountingDb.Ledgers.AsNoTracking().ToListAsync();
    }
}
