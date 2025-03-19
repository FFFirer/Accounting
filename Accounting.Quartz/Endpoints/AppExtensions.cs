using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Accounting.Quartz.Endpoints;

public static class AppExtensions
{
    public static IEndpointRouteBuilder MapAccountingQuartzApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGroup("/api")
            .MapJobDetailEndpoints()
            .MapJobEndpoints();
    }
}
