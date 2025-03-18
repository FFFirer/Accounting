using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Accounting.Common;

public static class EnumHelper
{
    public static string? GetDescription<T>(this T value) where T : Enum
    {
        var attr = GetCustomAttribute<T, DescriptionAttribute>(value);

        return attr?.Description;
    }

    private static TA? GetCustomAttribute<T, TA>(T value) where T : Enum where TA : Attribute
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);

        if (name is null) { return (TA?)null; }

        var field = type.GetField(name);

        if (field is null) { return (TA?)null; }

        return field.GetCustomAttribute<TA>();
    }

    public static string? GetDisplayName<T>(this T value) where T : Enum
    {
        return GetCustomAttribute<T, DisplayAttribute>(value)?.Name ?? GetCustomAttribute<T, DisplayNameAttribute>(value)?.DisplayName;
    }
}
