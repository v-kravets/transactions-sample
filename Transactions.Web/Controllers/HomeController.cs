using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Transactions.Web.Models;
using Transactions.Web.Extensions;

namespace Transactions.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Index(FileUploadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.FileInfo.IsGreaterThanBytes(1024 * 1024))
            {
                return BadRequest("File is over allowed limit");
            }
            
            return Ok();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
