using System;

namespace System;

public static class TypeHelper
{
    /// <summary>
    /// 获取指定泛型基类
    /// </summary>
    /// <param name="currentType"></param>
    /// <param name="genericBaseType"></param>
    /// <returns></returns>
    public static Type? FindGenericBaseType(this Type currentType, Type genericBaseType)
    {
        Type? type = currentType;
        while (type != null)
        {
            var genericType = type.IsGenericType ? type.GetGenericTypeDefinition() : null;
            if (genericType != null && genericType == genericBaseType)
            {
                return type;
            }
            type = type.BaseType;
        }
        return null;
    }
}
