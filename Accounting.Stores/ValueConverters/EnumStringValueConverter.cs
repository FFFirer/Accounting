using System;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Accounting.ValueConverters;

public class EnumStringValueConverter<T> : ValueConverter<T, string> where T : Enum
{
    public EnumStringValueConverter() : this(
        v => v.ToString(),
        s => (T)Enum.Parse(typeof(T), s)
    )
    {

    }

    public EnumStringValueConverter(Expression<Func<T, string>> convertToProviderExpression, Expression<Func<string, T>> convertFromProviderExpression, ConverterMappingHints? mappingHints = null) : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
    {
    }
}
