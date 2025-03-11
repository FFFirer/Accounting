using System;

namespace Accounting.Documents;

public interface IDocumentStore<T> where T : class, new()
{
    Task<T?> GetAsync(CancellationToken cancellationToken = default);

    Task CreateAsync(T content, CancellationToken cancellationToken = default);

    Task UpdateAsync(Action<T>? updateContent = null, CancellationToken cancellationToken = default);
}
