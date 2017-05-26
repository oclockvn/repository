using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace oclockvn.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Cut given string for shorter string
        /// </summary>
        /// <param name="s">Given string to cut</param>
        /// <param name="length">Max length to display</param>
        /// <param name="ellipsis">The end string if over length</param>
        /// <returns>The cutted string</returns>
        public static string Truncate(this string s, int length = 50, string ellipsis = "...")
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            if (s.Length <= length)
                return s;

            return s.Substring(0, length).Trim() + ellipsis;
        }

        /// <summary>
        /// Remove unicode character (unicode) of given string
        /// </summary>
        /// <param name="input">The input string to remove mark</param>
        /// <returns>The string without mark</returns>
        public static string RemoveMark(this string input)
        {
            var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            var formD = input.Normalize(NormalizationForm.FormD);

            return regex.Replace(formD, string.Empty)
                .Replace('\u0111', 'd')
                .Replace('\u0110', 'D');
        }

        /// <summary>
        /// Convert a string into friendly url form with hiphen between words
        /// </summary>
        /// <param name="input">The input string</param>
        /// <param name="length">The maximum length of result</param>
        /// <returns>The string result</returns>
        public static string ToFriendlyUrl(this string input, int length = 125)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            string slug = input.RemoveMark().ToLower();

            // remove invalid chars
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", string.Empty);

            // convert multiple spaces into one
            slug = Regex.Replace(slug, "\\s+", " ").Trim();
            slug = Regex.Replace(slug, "\\s", "-");
            slug = Regex.Replace(slug, "-+", "-");
            slug = slug.Trim('-');

            // cut and trim            
            return slug.Truncate(length, string.Empty);
        }

        /// <summary>
        /// Capital string like: 'lorem ipsum dolor sit amet' -> 'Lorem Ipsum Dolor Sit Amet'
        /// </summary>
        /// <param name="input">The input string</param>
        /// <returns>The capitalized string</returns>
        public static string ToCapitalize(this string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                return string.Empty;

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            return textInfo.ToTitleCase(input.Trim());
        }

        /// <summary>
        /// create an list string from given string, split by specify pattern
        /// </summary>
        /// <param name="input">The input string</param>
        /// <param name="splitPattern">The pattern used to split input string</param>
        /// <returns>The collection contains splited parts</returns>
        public static List<string> ToCollection(this string input, string splitPattern = "\r\n")
        {
            List<string> results = new List<string>();

            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                return results;

            var splits = Regex.Split(input.Trim(), splitPattern);
            if (splits != null && splits.Length > 0)
            {
                results = (from i in splits
                           where !string.IsNullOrEmpty(i) && !string.IsNullOrWhiteSpace(i)
                           select i).ToList();
            }

            return results;
        }
    }
}
