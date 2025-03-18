using System;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Accounting.ValueConverters;

public class DateTimeOffsetUtcConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
    static readonly TimeSpan ZeroOffset = TimeSpan.Zero;

    static readonly Expression<Func<DateTimeOffset, DateTimeOffset>> ConvertToUTC =
        (DateTimeOffset time) => time.Offset == ZeroOffset ? time : time.ToOffset(ZeroOffset);

    static readonly Expression<Func<DateTimeOffset, DateTimeOffset>> ConvertFromUTC =
        (DateTimeOffset time) => time.Offset != ZeroOffset ? time : time.ToLocalTime();

    public DateTimeOffsetUtcConverter() : base(ConvertToUTC, ConvertFromUTC)
    {

    }
}
