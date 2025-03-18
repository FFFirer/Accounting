using System;
using System.ComponentModel.DataAnnotations;

using Accounting.Asset;
using Accounting.Books;

using Mapster;

using Microsoft.AspNetCore.Components;

namespace Accounting.Web.Components.Books.Pages;

public partial class CreateLedgerRecord : ComponentBase
{
    private string? ErrorMessage { get; set; }

    [Parameter]
    public string? LedgerId { get; set; }

    public Ledger? Ledger { get; set; }

    [Inject]
    public AccountingDbContext AccountingDb { get; set; } = default!;

    private CreateLedgerRecordModel Input { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        if (string.IsNullOrWhiteSpace(this.LedgerId) == false)
        {
            if (Guid.TryParse(LedgerId, out var id))
            {
                this.Ledger = await AccountingDb.Ledgers.FindAsync(id);
            }
        }

        if (this.Ledger is null)
        {
            return;
        }
    }

    private class CreateLedgerRecordModel
    {
        public CreateLedgerRecordModel()
        {
            RecordTime = DateTimeOffset.UtcNow;
            FlowDirection = AssetFlowDirection.Expenditure;
        }

        [Required]
        public DateTimeOffset? RecordTime { get; set; }

        [Required]
        public AssetFlowDirection? FlowDirection { get; set; } 

        [Required]
        [Range(minimum:0, maximum: (double)decimal.MaxValue)]
        public decimal? Amount { get; set; }

        public string? AssetAccountId { get; set; }

        public string? Remark { get; set; }
    }

    private async Task SubmitCreateLedgerRecord()
    {
        if (this.Ledger is null)
        {
            ErrorMessage = "Error: " + "账本不存在";
            return;
        }

        var ledgerRecord = this.Input.Adapt<LedgerRecord>();
        ledgerRecord.Ledger = this.Ledger;
        ledgerRecord.AssetAccount = this.Ledger.AssetAccounts?.Where(x => x.Id == this.Input.AssetAccountId).FirstOrDefault();

        AccountingDb.LedgerRecords.Add(ledgerRecord);
        await AccountingDb.SaveChangesAsync();

        this.Input = new();
    }
}
