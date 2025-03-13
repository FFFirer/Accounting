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

    Task<List<StorageBucket>> ExecuteQueryBucketsAsync(Expression<Func<StorageBucket, bool>>? predicate = null, CancellationToken cancellationToken = default);
    Task<PageList<StorageBucket>> ExecutePageBucketsAsync(StorageBucketQueryFilter filter, IPageQuery pageQuery, CancellationToken cancellationToken = default);    
    Task<StorageBucket?> FindBucketAsync(string name, CancellationToken cancellationToken);
};

public interface IFileStorageStore : IQueryableFileStorageStore
{
    Task UpdateAsync(FileInformation fileInformation, CancellationToken cancellationToken);
    Task CreateAsync(FileInformation fileInformation, CancellationToken cancellationToken);

    Task CreateBucketAsync(StorageBucket bucket, CancellationToken cancellationToken);
    Task<StorageBucket> GetOrCreateBucketAsync(string bucketName, CancellationToken cancellationToken);
}
