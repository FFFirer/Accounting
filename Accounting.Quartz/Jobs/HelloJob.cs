using System;
using System.Collections.Generic;
using System.Text;

using Quartz;

namespace Accounting.Quartz.Jobs;

public class HelloJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        throw new NotImplementedException();
    }
}
