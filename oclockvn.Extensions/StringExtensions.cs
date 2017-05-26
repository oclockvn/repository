using System.Text;
using System.Text.RegularExpressions;

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
    }
}
