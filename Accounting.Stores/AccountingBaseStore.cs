using System;

using Accounting.Common;

namespace Accounting;

public class AccountingBaseStore : StoreBase
{
    protected virtual AccountingDbContext Context { get; set; }

    public AccountingBaseStore(AccountingDbContext context)
    {
        Context = context;
    }

    public override async Task DisposeManagedAsync()
    {
        await Context.DisposeAsync();
    }

    protected override void DisposeManaged()
    {
        Context.Dispose();
    }

    protected override Task SaveChanges(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
    }
}
