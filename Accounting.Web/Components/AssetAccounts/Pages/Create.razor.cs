using System;
using System.ComponentModel.DataAnnotations;

using Accounting.Asset;

using Mapster;

using Microsoft.AspNetCore.Components;

namespace Accounting.Web.Components.AssetAccounts.Pages;

public partial class Create : ComponentBase
{
    private string? Message { get; set; }

    [Inject]
    protected virtual AccountingDbContext AccountingDb { get; set; } = default!;
    private InputModel Input { get; set; } = new();

    private async Task SubmitCreate()
    {
        var account = this.Input.Adapt<AssetAccount>();

        account.Id = Guid.NewGuid().ToString();

        AccountingDb.AssetAccounts.Add(account);

        await AccountingDb.SaveChangesAsync();
        
        this.Input = new();
        this.Message = "添加成功";
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
