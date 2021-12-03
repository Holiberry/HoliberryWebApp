using System;
using System.Collections.Generic;
using System.Text;

namespace Holiberry.Api.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string Format(this TimeSpan obj)
        {
            if (obj == null)
                return "";

            StringBuilder sb = new StringBuilder();
            if (obj.Days != 0)
            {
                sb.Append(obj.Days);
                sb.Append("d");
            }
            if (obj.Hours != 0)
            {
                int totalHours = obj.Hours;

                //if (obj.Days > 0)
                //    totalHours += obj.Days * 24;

                sb.Append(totalHours);
                sb.Append("h");
            }
            if (obj.Minutes != 0 || sb.Length != 0)
            {
                sb.Append(obj.Minutes);
                sb.Append("m");
            }
            if (obj.Seconds != 0 || sb.Length != 0)
            {
                sb.Append(obj.Seconds);
                sb.Append("s");
            }
            //if (obj.Milliseconds != 0 || sb.Length != 0)
            //{
            //    sb.Append(obj.Milliseconds);
            //    sb.Append(" ");
            //    sb.Append("ms");
            //}
            if (sb.Length == 0)
            {
                sb.Append(0);
            }
            return sb.ToString();
        }


    }
}
