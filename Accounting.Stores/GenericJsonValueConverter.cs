using System;
using System.Linq.Expressions;
using System.Text.Json;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Accounting;

public class GenericJsonValueConverter<T> : ValueConverter<T?, string?> where T : class
{
    public GenericJsonValueConverter(
                Expression<Func<T?, string?>> convertToProviderExpression,
                Expression<Func<string?, T?>> convertFromProviderExpression,
                ConverterMappingHints? mappingHints = null)
                : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
    {
        
    }

    public static string? SerializeToString(T? model, JsonSerializerOptions? options = null)
    {
        if (model is null) { return (string?)null; }

        return JsonSerializer.Serialize(model, options);
    }

    public static T? DeserializeToModel(string? provider, JsonSerializerOptions? options = null)
    {
        if (string.IsNullOrWhiteSpace(provider)) { return null; }

        return JsonSerializer.Deserialize<T?>(provider, options);
    }

    public static GenericJsonValueConverter<T> Default(JsonSerializerOptions? options = null, ConverterMappingHints? mappingHints = null)
    {
        return new GenericJsonValueConverter<T>(model => SerializeToString(model, options), provider => DeserializeToModel(provider, options), mappingHints);
    }
}
