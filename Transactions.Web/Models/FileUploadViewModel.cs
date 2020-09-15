using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Transactions.Web.ValidationAttributes;

namespace Transactions.Web.Models
{
    public class FileUploadViewModel
    {
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new[] { ".csv", ".xml" })]
        public IFormFile FileInfo { get; set; }
    }
}