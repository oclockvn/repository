using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oclockvn.Extensions.i18n;

namespace oclockvn.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Try parse input datetime string into datetime using specific culture
        /// </summary>
        /// <param name="dateString">a string of datetime to parse</param>
        /// <param name="format">dateString input format</param>
        /// <param name="culture">specify culture info to parse</param>
        /// <returns>return DateTime if parse success, otherwise return null</returns>
        public static DateTime? ToDateTime(this string dateString, string format = "dd/MM/yyyy", string culture = "vi-vn")
        {
            DateTime dt;
            var succeed = DateTime.TryParseExact(dateString, format, CultureInfo.CreateSpecificCulture(culture), DateTimeStyles.None, out dt);

            if (succeed)
                return dt;

            return null;
        }

        private static string GetResource(string name, string culture = "vi") => AppResources.ResourceManager.GetString(name, new CultureInfo(culture));

        /// <summary>
        /// Return an user-friendly date time like "2 days ago"
        /// </summary>
        /// <param name="dateTime">a time in past to compare</param>
        /// <param name="culture">specific culture to display. Default is Vietnamese.
        /// </param>
        /// <returns>a string of date time formatted</returns>
        public static string ToTimeAgo(this DateTime dateTime, string culture = "vi")
        {
            if (dateTime > DateTime.Now)
                return string.Empty;

            string result = string.Empty;
            var ts = DateTime.Now.Subtract(dateTime);
            
            if (ts <= TimeSpan.FromSeconds(60))
            {
                result = ts.Seconds > 1
                    ? string.Format(GetResource("TimeAgo_Seconds", culture), ts.Seconds)
                    : GetResource("TimeAgo_Second", culture);
            }
            else if (ts <= TimeSpan.FromMinutes(60))
            {
                result = ts.Minutes > 1
                    ? string.Format(GetResource("TimeAgo_Minutes", culture), ts.Minutes)
                    : GetResource("TimeAgo_Minute", culture);
            }
            else if (ts <= TimeSpan.FromHours(24))
            {
                result = ts.Hours > 1
                    ? string.Format(GetResource("TimeAgo_Hours", culture), ts.Hours)
                    : GetResource("TimeAgo_Hour", culture);
            }
            else if (ts <= TimeSpan.FromDays(30))
            {
                result = ts.Days > 1
                    ? string.Format(GetResource("TimeAgo_Days", culture), ts.Days)
                    : GetResource("TimeAgo_Day", culture);
            }
            else if (ts <= TimeSpan.FromDays(365))
            {
                result = ts.Days > 30
                    ? string.Format(GetResource("TimeAgo_Months", culture), ts.Days / 30)
                    : GetResource("TimeAgo_Month", culture);
            }
            else
            {
                result = ts.Days > 365
                    ? string.Format(GetResource("TimeAgo_Years", culture), ts.Days / 365)
                    : GetResource("TimeAgo_Year", culture);
            }

            return result;
        }
    }
}
