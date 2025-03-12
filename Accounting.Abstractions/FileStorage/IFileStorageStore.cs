using System;
using System.Linq.Expressions;

using Accounting.Common;
using Accounting.Data;

namespace Accounting.FileStorage;

public interface IQueryableFileStorageStore
{
    IQueryable<FileInformation> Source { get; }

    Task<List<FileInformation>> ExecuteQueryAsync(Expression<Func<FileInformation, bool>>? predicate = null, CancellationToken cancellationToken = default);

    Task<PageList<FileInformation>> ExecutePageAsync(FileInfomationQueryFilter filter, IPageQuery pageQuery, CancellationToken cancellationToken = default);

    Task<FileInformation?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface IFileStorageStore : IQueryableFileStorageStore
{
    Task<FileInformation> CreateAsync(CancellationToken cancellationToken);

    Task SaveAsync(FileInformation fileInformation, CancellationToken cancellationToken);
}
