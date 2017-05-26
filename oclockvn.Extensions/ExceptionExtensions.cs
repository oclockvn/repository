using System;

namespace oclockvn.Extensions
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// Get error message of an exception
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <param name="tag">The display tag</param>
        /// <param name="loop">The stop loop</param>
        /// <returns>a string of message</returns>
        public static string ToErrorMessage(this Exception ex, string tag = "Exception", int loop = 1)
        {
            return (ex == null || loop >= 5) 
                ? string.Empty
                : $"[{tag}] => {ex.Message} \r\n {ex.InnerException?.ToErrorMessage($"Inner {loop}", loop++)}";
        }
    }
}
