using Accounting.Data;

namespace Accounting.Quartz;

public class JobErrorDescriber
{
    public Error NotExistJobForClassName(string className) => new(nameof(NotExistJobForClassName), $"Not exists job for class name: {className}");
}