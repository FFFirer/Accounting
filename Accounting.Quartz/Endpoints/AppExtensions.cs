using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Accounting.Quartz.Endpoints;

public static class AppExtensions
{
    public static IEndpointRouteBuilder MapAccountingQuartzApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var apiGroup = endpoints.MapGroup("/api");
        apiGroup.MapJobDetailEndpoints().WithTags("JobDetail");
        apiGroup.MapJobEndpoints().WithTags("Job");

        return apiGroup;
    }
}
