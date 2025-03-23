using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Quartz;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAccountingJobs(this IServiceCollection services)
    {
        var jobs = JobScanner.LoadJobTypes(typeof(ServiceCollectionExtensions).Assembly);

        services.AddSingleton<JobIndexes>(new JobIndexes(jobs));
        services.AddSingleton<JobErrorDescriber>();

        return services;
    }
}
