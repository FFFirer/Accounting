using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Accounting.Quartz;

public static class JobScanner
{
    public static List<Type> LoadJobTypes(Assembly assembly)
    {
        return (from type in assembly.GetTypes()
                where type.GetInterface("IJob") != null
                select type).ToList();
    }
}
