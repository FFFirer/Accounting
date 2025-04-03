using Accounting.FileStorage;
using Accounting.Imports;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Web.Components.Files.Pages;

public partial class ImportRecords : ComponentBase, IAsyncDisposable
{
    [Inject]
    public AccountingDbContext DbContext { get; set; } = default!;

    private PaginationState pagination = new PaginationState { ItemsPerPage = 10 };

    public async ValueTask DisposeAsync()
    {
        await this.DbContext.DisposeAsync();
    }

    private string GetRowClass(ImportRecordDto dto) => "hover:bg-base-300";

    public async ValueTask<GridItemsProviderResult<ImportRecordDto>> GetItems(GridItemsProviderRequest<ImportRecordDto> request)
    {
        var query = this.DbContext.ImportRecords.AsNoTracking();

        var totalCount = await query.CountAsync();

        query = query.Page(pagination.CurrentPageIndex, pagination.ItemsPerPage);

        var datas = await query.Include(x => x.File)
            .Select(x => new ImportRecordDto
            {
                Id = x.Id,
                FileId = x.File == null ? null : x.File.Id,
                FileName = x.File == null ? null : x.File.OriginalFileName,
                CreatedTime = x.CreatedTime,
                ChannelCode = x.Channel == null ? null : x.Channel.Code,
                ChannelName = x.Channel == null ? null : x.Channel.Name,
                Status = x.Status,
                Count = x.Count
            }).ToListAsync();

        return new GridItemsProviderResult<ImportRecordDto>
        {
            TotalItemCount = totalCount,
            Items = datas
        };
    }
}

public class ImportRecordDto
{
    public long Id { get; set; }

    public Guid? FileId { get; set; }
    public string? FileName { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public string? ChannelCode { get; set; }
    public string? ChannelName { get; set; }

    public ImportStatus? Status { get; set; }

    public int Count { get; set; }
}