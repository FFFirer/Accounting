using System;
using System.ComponentModel.DataAnnotations;

using Accounting.Asset;

using Microsoft.AspNetCore.Components;

namespace Accounting.Web.Components.AssetAccounts.Pages;

public partial class Create : ComponentBase
{
    [Inject]
    protected virtual AccountingDbContext AccountingDb { get; set; } = default!;
    private InputModel Input { get; set; } = new();

    private async Task SubmitCreate()
    {
        await Task.CompletedTask;
    }

    private class InputModel
    {
        [Required]
        public string? Name { get; set; }

        public string? Icon { get; set; }

        [Required]
        public AssetAccountType? Type { get; set; }

        public bool IncludedInTotalAssets { get; set; }
    }
}
