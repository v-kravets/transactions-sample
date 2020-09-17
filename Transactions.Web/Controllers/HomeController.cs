using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Transactions.Parsing.Abstract;
using Transactions.Web.Models;
using Transactions.Web.Extensions;

namespace Transactions.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IParsingStrategyFactory _parsingStrategyFactory;

        public HomeController(ILogger<HomeController> logger, IParsingStrategyFactory parsingStrategyFactory)
        {
            _logger = logger;
            _parsingStrategyFactory = parsingStrategyFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(FileUploadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.FileInfo.IsGreaterThanBytes(1024 * 1024))
            {
                return BadRequest("File is over allowed limit");
            }

            var strategy = _parsingStrategyFactory.GetStrategy(Path.GetExtension(model.FileInfo.FileName));
            var parsingResults = await strategy.ParseTransactionsAsync(model.FileInfo.OpenReadStream());
            if (!parsingResults.Success)
            {
                return BadRequest(parsingResults.Error);
            }

            //todo save into database
            
            return Ok();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
