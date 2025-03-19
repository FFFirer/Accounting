using System.Collections;
using System.CommandLine;

namespace Accounting.Migrator;

public static class TableDisplay
{
    static Type? GetEnumerableItemType(Type type)
    {
        var enumerableT = type.GetInterfaces().Where(x => x.IsGenericType && x.Name == typeof(IEnumerable<>).Name).FirstOrDefault();

        return enumerableT?.GenericTypeArguments?.FirstOrDefault();
    }

    public static void PrintAsTable<T>(this IConsole console, T? data)
    {
        var itemType = GetEnumerableItemType(typeof(T));

        if (itemType is null)
        {
            PrintObjectAsTable(console, data);
        }
        else
        {
            PrintItemsAsTable(console, itemType, data);
        }
    }

    private static void PrintItemsAsTable<T>(IConsole console, Type itemType, T? data)
    {
        var itemProperties = itemType.GetProperties();
        var headerLabels = itemProperties.Select(x => x.Name).ToArray();

        var enumeratorMethod = typeof(T).GetMethod("GetEnumerator");

        if (data is null || enumeratorMethod is null)
        {
            // 只打印表头
            var headerWidths = itemProperties.Select(x => x.Name.Length).ToArray();

            console.PrintRow(headerWidths, headerLabels);
            console.PrintSeparator(headerWidths);

            return;
        }

        var enumerator = (IEnumerator?)enumeratorMethod.Invoke(data, new object[0]);

        if (enumerator is null)
        {
            throw new InvalidOperationException("Cannot get IEnumerator");
        }

        var columnWidths = itemProperties.Select(x => x.Name.Length).ToArray();
        var rows = new List<string[]>();

        while (enumerator.MoveNext())
        {
            var row = new string[itemProperties.Length];
            rows.Add(row);

            for (int i = 0; i < itemProperties.Length; i++)
            {
                row[i] = (itemProperties[i].GetValue(enumerator.Current)?.ToString() ?? string.Empty);
                columnWidths[i] = Math.Max(columnWidths[i], row[i].Length);
            }
        }

        console.PrintRow(columnWidths, headerLabels);
        console.PrintSeparator(columnWidths);

        foreach (var row in rows)
        {
            console.PrintRow(columnWidths, row);
        }
    }

    private static void PrintObjectAsTable<T>(IConsole console, T? data)
    {
        var properties = typeof(T).GetProperties();

        var rows = properties
            .Select(prop => new
            {
                Name = prop.Name,
                Value = prop.GetValue(data)?.ToString()
            })
            .ToList();

        var columnWidths = new int[] { rows.Max(x => x.Name.Length), rows.Max(x => x.Value?.Length ?? 0) };

        var headerLabels = new string[] { "Name", "Value" };
        var rowT = string.Join(" ", Enumerable.Range(0, headerLabels.Length).Select(index => $"{{{{ {index},{{index}} }}}}"));

        console.PrintRow(columnWidths, headerLabels);
        console.PrintSeparator(columnWidths);

        if (rows is not null)
        {
            foreach (var row in rows)
            {
                console.PrintRow(columnWidths, new string[] { row.Name, row.Value ?? string.Empty });
            }
        }
    }

    private static void PrintRow(this IConsole console, int[] columnWidth, object[] values)
    {
        var t = string.Join(" ", Enumerable.Range(0, columnWidth.Length).Select(x => $"{{{x},-{columnWidth[x]}}}"));

        var v = string.Format(t, values);

        console.WriteLine(v);
    }

    private static void PrintSeparator(this IConsole console, int[] columnWidths)
    {
        foreach (var width in columnWidths)
        {
            console.Write(new string('-', width + 1));
        }
        console.WriteLine("-");
    }


}
