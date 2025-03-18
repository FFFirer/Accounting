using System;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Accounting.ValueConverters;

public class DateTimeUtcConverter : ValueConverter<DateTime, DateTime>
    {
        static readonly Expression<Func<DateTime, DateTime>> ConvertToUTC =
            (DateTime time) => time.Kind == DateTimeKind.Utc || time.Kind == DateTimeKind.Unspecified ?
                                time :
                                TimeZoneInfo.ConvertTimeToUtc(time);

        static readonly Expression<Func<DateTime, DateTime>> ConvertFromUTC =
            (DateTime time) => time.Kind != DateTimeKind.Utc || time.Kind == DateTimeKind.Unspecified ?
                                time :
                                time.ToLocalTime();

        public DateTimeUtcConverter() : base(ConvertToUTC, ConvertFromUTC)
        {

        }
    }
