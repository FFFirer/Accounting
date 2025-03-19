using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Quartz;

public sealed class JobIndexes
{
    private readonly List<Type> _types;

    public int TotalCount => _types.Count;
    public IEnumerable<Type> Types => _types;

    public JobIndexes(IEnumerable<Type> types)
    {
        _types = types.ToList();
        NameIndex = types.GroupBy(x => x.FullName).ToDictionary(x => x.Key!, y => y.First());
    }

    
    public IReadOnlyDictionary<string, Type> NameIndex { get; private set; }
}
