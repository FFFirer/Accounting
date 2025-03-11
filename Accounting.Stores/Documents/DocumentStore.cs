using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Accounting.Documents;

public class DocumentStore<T> : IDocumentStore<T> where T : class, new()
{
    protected virtual AccountingDbContext Context { get; set; }
    protected virtual bool AutoSaveChanges { get; set; } = true;

    public DocumentStore(AccountingDbContext context)
    {
        Context = context;
    }

    public async Task CreateAsync(T content, CancellationToken cancellationToken)
    {
        var current = FindTypedAsync(cancellationToken);

        if (current is not null)
        {
            throw new InvalidOperationException($"{typeof(T).FullName} 已存在记录: {current.Id}");
        }

        Context.Add(new Document<T>(content));

        await SaveChanges(cancellationToken);
    }

    private Task SaveChanges(CancellationToken cancellationToken)
    {
        return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
    }

    private async Task<Document<T>?> FindTypedAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<Document<T>>().SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetAsync(CancellationToken cancellationToken)
    {
        var document = await Context.Set<Document<T>>().AsNoTracking().SingleOrDefaultAsync(cancellationToken);
        return document?.Content;
    }

    public async Task UpdateAsync(Action<T>? updateContent, CancellationToken cancellationToken)
    {
        var current = await FindTypedAsync(cancellationToken);

        if (current is null)
        {
            throw new InvalidOperationException($"{typeof(T).FullName} 记录不存在");
        }

        updateContent?.Invoke(current.Content ?? new T());

        await SaveChanges(cancellationToken);
    }
}
