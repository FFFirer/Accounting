using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using Accounting.Common;
using Accounting.Data;

using AppAny.Quartz.EntityFrameworkCore.Migrations;

using Mapster;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

using Quartz;

namespace Accounting.Quartz.Endpoints;

public static class JobDetailEndpoints
{
    public static IEndpointConventionBuilder MapJobDetailEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var endpointsGroup = endpoints.MapGroup("/JobDetail");

        endpointsGroup.MapGet("/Query", QueryJobDetails);
        endpointsGroup.MapPost("/Create", CreateJobDetail);

        return endpointsGroup;
    }

    public record CreateJobDetailRequest(
        [Required(AllowEmptyStrings = false)] string ClassName,
        string Group,
        [Required(AllowEmptyStrings = false)] string Name);

    private static async Task<Result> CreateJobDetail(
        [FromServices] JobErrorDescriber errorDescriber,
        [FromServices] JobIndexes indexes,
        [FromServices] ISchedulerFactory schedulerFactory,
        [FromBody] CreateJobDetailRequest request,
        [FromServices] ICancellationTokenProvider cancellation)
    {
        if (indexes.NameIndex.TryGetValue(request.ClassName, out var jobType) == false)
        {
            return Result.Failed(errorDescriber.NotExistJobForClassName(request.ClassName));
        }

        var jobDetail = JobBuilder.Create(jobType).WithIdentity(request.Name, request.Group).Build();

        var sched = await schedulerFactory.GetScheduler();


        await sched.AddJob(jobDetail, true, true, cancellation.Token);

        return Result.Success();
    }

    private static async Task<PageList<JobDetailDto>> QueryJobDetails(
        [FromQuery] int page, [FromQuery] int size,
        [FromServices] AccountingQuartzDbContext context,
        [FromServices] ICancellationTokenProvider cancellation)
    {
        var query = context.Set<QuartzJobDetail>().AsNoTracking();

        var total = await query.CountAsync(cancellation.Token);

        var items = await query.Page(page, size).Select(x => x.Adapt<JobDetailDto>()).ToListAsync(cancellation.Token);

        return new PageList<JobDetailDto>(total, items);
    }
}

public class JobDetailDto
{
    public string? SchedulerName { get; set; }

    public string? JobName { get; set; }

    public string? JobGroup { get; set; }

    public string? Description { get; set; }

    public string? JobClassName { get; set; }

    public bool IsDurable { get; set; }

    public bool IsNonConcurrent { get; set; }

    public bool IsUpdateData { get; set; }

    public bool RequestsRecovery { get; set; }
}