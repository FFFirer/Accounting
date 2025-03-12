using System;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public class InjectionBuilder
{
    public IServiceCollection Services { get; protected set; }

    private List<Action<Assembly, Type>> _injections = [];

    private List<Assembly> _assemblies { get; set; }

    public InjectionBuilder(IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        Services = services;
        _assemblies = assemblies.ToList();
    }

    public void Register()
    {
        foreach (var assembly in _assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                foreach (var injection in _injections)
                {
                    injection(assembly, type);
                }
            }
        }
    }

    internal void Add(Action<Assembly, Type> action) => _injections.Add(action);
}

public static class InjectionHelper
{
    public static InjectionBuilder AddInjection(this InjectionBuilder builder, Func<Assembly, Type, bool> predicte, Action<Assembly, Type> injection)
    {
        var action = (Assembly assembly, Type type) =>
        {
            if (predicte(assembly, type))
            {
                injection(assembly, type);
            }
        };

        builder.Add(action);

        return builder;
    }

    public static InjectionBuilder AddInjectionFromAssemblies(this IServiceCollection services, Func<IEnumerable<Assembly>> assemblyGenerator)
    {
        return new InjectionBuilder(services, assemblyGenerator?.Invoke() ?? []);
    }
}
