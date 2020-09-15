using System;
using System.Text;

namespace Transactions.Utils
{
    public static class ExpandExceptionExtensions
    {
        /// <summary>
        /// Expands whole exception hierarchy messages and stack traces into single string
        /// </summary>
        /// <param name="ex">Exception to expand</param>
        public static string ExpandException(this Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            return ExpandException(ex, ref stringBuilder).ToString();
        }

        private static StringBuilder ExpandException(Exception ex, ref StringBuilder stringBuilder)
        {
            stringBuilder.Append("\r\nException Message: ");
            stringBuilder.Append(ex.Message);
            stringBuilder.Append("\r\nStack Trace: ");
            stringBuilder.Append(ex.StackTrace);
            return ex.InnerException != null
                ? ExpandException(ex.InnerException, ref stringBuilder)
                : stringBuilder;
        }
    }
}
