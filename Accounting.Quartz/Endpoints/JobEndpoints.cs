using Accounting.Data;

using Mapster;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Accounting.Quartz.Endpoints;

public static class JobEndpoints
{
    public static IEndpointConventionBuilder MapJobEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var endpointsGroup = endpoints.MapGroup("/Job").WithTags("Job");

        endpointsGroup.MapGet("/Query", QueryJobs);

        return endpointsGroup;
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
