using System;
using System.Linq.Expressions;

using Accounting.Common;
using Accounting.Data;
using Accounting.FileStorage;

using Microsoft.EntityFrameworkCore;

namespace Accounting;

public class FileStorageStore : StoreBase, IFileStorageStore
{
    protected virtual AccountingDbContext Context { get; set; }

    public FileStorageStore(AccountingDbContext context)
    {
        Context = context;
    }

    public IQueryable<FileInformation> Source => Context.Set<FileInformation>().AsNoTracking();

    public Task<FileInformation> CreateAsync(CancellationToken cancellationToken)
    {
        ThrowIfDisposed();

        return Task.FromResult(new FileInformation());
    }

    public async Task<PageList<FileInformation>> ExecutePageAsync(FileInfomationQueryFilter filter, IPageQuery pageQuery, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        var query = this.Source;

        var totalCount = await query.CountAsync(cancellationToken);

        var page = new PageList<FileInformation>(totalCount);

        if (totalCount > 0)
        {
            page.Datas.AddRange(await query.DoPageAsync(pageQuery).ToListAsync(cancellationToken));
        }

        return page;
    }

    public async Task<List<FileInformation>> ExecuteQueryAsync(Expression<Func<FileInformation, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        var query = this.Source;

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<FileInformation?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        return await Context.FileInformations.FindAsync([id], cancellationToken: cancellationToken);
    }

    public Task UpdateAsync(FileInformation fileInformation, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        var entry = Context.Attach(fileInformation);

        entry.State = EntityState.Modified;

        return SaveChanges(cancellationToken);
    }

    protected override Task SaveChanges(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return AutoSaveChanges ? Context.SaveChangesAsync(cancellationToken) : Task.CompletedTask;
    }

    public Task CreateAsync(FileInformation fileInformation, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        Context.Add(fileInformation);

        return SaveChanges(cancellationToken);
    }

    public Task CreateBucketAsync(StorageBucket bucket, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        Context.Add(bucket);

        return SaveChanges(cancellationToken);
    }

    public async Task<StorageBucket> GetOrCreateBucketAsync(string bucketName, CancellationToken cancellationToken)
    {
        var current = await FindBucketAsync(bucketName, cancellationToken);

        if (current is null)
        {
            current = new StorageBucket(bucketName);
            await CreateBucketAsync(current, cancellationToken);
        }

        return current;
    }

    public async Task<List<StorageBucket>> ExecuteQueryBucketsAsync(Expression<Func<StorageBucket, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        var query = Context.StorageBuckets.AsNoTracking();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<PageList<StorageBucket>> ExecutePageBucketsAsync(StorageBucketQueryFilter filter, IPageQuery pageQuery, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        var query = this.Context.StorageBuckets;

        var totalCount = await query.CountAsync(cancellationToken);

        var page = new PageList<StorageBucket>(totalCount);

        if (totalCount > 0)
        {
            page.Datas.AddRange(await query.DoPageAsync(pageQuery).ToListAsync(cancellationToken));
        }

        return page;
    }

    public async Task<StorageBucket?> FindBucketAsync(string name, CancellationToken cancellationToken)
    {
        return await Context.StorageBuckets.FindAsync(name, cancellationToken);
    }
}
