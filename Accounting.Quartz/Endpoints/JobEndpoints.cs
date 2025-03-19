using Accounting.Data;

using Mapster;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Accounting.Quartz.Endpoints;

public static class JobEndpoints
{
    public static IEndpointRouteBuilder MapJobEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var endpointsGroup = endpoints.MapGroup("/Job");

        endpointsGroup.MapGet("/Query", QueryJobs);

        return endpoints;
    }

    private static async Task<PageList<JobDefinationDto>> QueryJobs([FromQuery] int page, [FromQuery]int size, [FromServices] JobIndexes indexes)
    {
        var query = indexes.Types;

        var total = indexes.TotalCount ;

        var items = query.Page(page, size).Select(x => x.Adapt<JobDefinationDto>()).ToList();

        return new PageList<JobDefinationDto>(total, items);
    }
}

public class JobDefinationDto
{
    public string? Namespace { get; set; }
    public string? FullName { get; set; }
}
