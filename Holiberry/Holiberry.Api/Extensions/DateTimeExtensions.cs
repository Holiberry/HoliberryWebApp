using System;
using System.Collections.Generic;
using System.Text;

namespace Holiberry.Api.Extensions
{
    public static class DateTimeExtensions
    {
        private static DateTimeOffset _emptyDateTimeOffset = new DateTimeOffset();
        private static DateTime _emptyDateTime = new DateTime();



        public const string LongDateFormat = "dd-MM-yyyy HH:mm";
        public const string LongDateWithSecondsFormat = "dd-MM-yyyy HH:mm:ss";
        public const string DateOnlyFormat = "dd-MM-yyyy";


        public static string ToLocalTimeString(this DateTimeOffset dt, string format = LongDateFormat)
        {
            if (dt == _emptyDateTimeOffset)
                return string.Empty;

            return dt.ToLocalTime().ToString(format);
        }

        public static string ToLocalTimeString(this DateTimeOffset? dt, string format = LongDateFormat) => dt switch
        {
            null => string.Empty,
            _ => dt.Value.ToLocalTimeString(format)
        };

        public static string ToLocalTimeString(this DateTime dt, string format = LongDateFormat)
        {
            if (dt == _emptyDateTime)
                return string.Empty;

            return dt.ToLocalTime().ToString(format);
        }

        public static string ToLocalTimeString(this DateTime? dt, string format = LongDateFormat) => dt switch
        {
            null => string.Empty,
            _ => dt.Value.ToLocalTimeString(format)
        };










        /// <summary>
        /// Od Listopada 2021
        /// </summary>
        public static List<DateTime> HolidaysDates = new List<DateTime>()
        {
            new DateTime(2021, 11, 1),
            new DateTime(2021, 11, 11),
            new DateTime(2021, 12, 25),
            new DateTime(2021, 12, 26),

            new DateTime(2022, 1, 1),
            new DateTime(2022, 1, 6),
            new DateTime(2022, 4, 17),
            new DateTime(2022, 4, 18),
            new DateTime(2022, 5, 1),
            new DateTime(2022, 5, 3),
            new DateTime(2022, 6, 5),
            new DateTime(2022, 6, 16),
            new DateTime(2022, 8, 15),
            new DateTime(2022, 11, 1),
            new DateTime(2022, 11, 11),
            new DateTime(2022, 12, 25),
            new DateTime(2022, 12, 26),
        };



    }
}
