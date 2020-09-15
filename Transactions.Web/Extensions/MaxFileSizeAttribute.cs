using Microsoft.AspNetCore.Http;

namespace Transactions.Web.Extensions
{
    public static class MaxFileSizeExtension
    {
        public static bool IsGreaterThanBytes(this IFormFile file, int bytesCount)
        {
            return file.Length > bytesCount;
        }
    }
}