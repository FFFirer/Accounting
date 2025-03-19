using System;
using System.Collections.Generic;
using System.Text;

using Accounting.Common;
using Accounting.Data;

using AppAny.Quartz.EntityFrameworkCore.Migrations;

using Mapster;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Quartz.Endpoints;

public static class TriggerEndpoints
{
    public static IEndpointRouteBuilder MapTriggerEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var endpointsGroup = endpoints.MapGroup("/Trigger");

        endpointsGroup.MapGet("/Query", QueryTriggers);

        return endpoints;
    }

    private static async Task<PageList<TriggerDto>> QueryTriggers([FromQuery] int page, [FromQuery] int size, [FromServices] AccountingQuartzDbContext context, [FromServices] ICancellationTokenProvider cancellation)
    {
        var query = context.Set<QuartzTrigger>().AsNoTracking();

        var totalCount = await query.CountAsync(cancellation.Token);

        var pageQuery = query.Page(page, size);

        var items = await pageQuery.Select(x => x.Adapt<TriggerDto>()).ToListAsync(cancellation.Token);

        return new PageList<TriggerDto>(totalCount, items);
    }
}

public class TriggerDto
{
    public string? SchedulerName { get; set; }

    public string? TriggerName { get; set; }

    public string? TriggerGroup { get; set; }

    public string? JobName { get; set; }

    public string? JobGroup { get; set; }

    public string? Description { get; set; }

    public long? NextFireTime { get; set; }

    public long? PreviousFireTime { get; set; }

    public int? Priority { get; set; }

    public string? TriggerState { get; set; }

    public string? TriggerType { get; set; }

    public long StartTime { get; set; }

    public long? EndTime { get; set; }

    public string? CalendarName { get; set; }

    public short? MisfireInstruction { get; set; }
}