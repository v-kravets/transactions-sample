using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Transactions.Model.Concrete;
using Transactions.Services.Abstract;
using Transactions.Web.Models.Api;

namespace Transactions.Web.Controllers
{
    
    [Route("api")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        private readonly ICurrencyTransactionsServices _currencyTransactionsServices;
        public HomeApiController(ICurrencyTransactionsServices currencyTransactionsServices)
        {
            _currencyTransactionsServices = currencyTransactionsServices;
        }

        [Route("get-by-currency/{currency}")]
        [HttpGet]
        [ProducesResponseType(typeof(CurrencyTransactionWebApi[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByCurrency(string currency)
        {
            var transactions = await _currencyTransactionsServices.CurrencyTransactionService.GetByCurrencyAsync(currency);
            return Ok(transactions.Select(CurrencyTransactionWebApi.FromDomainModel).ToArray());
        }
        
        [Route("get-by-date-range")]
        [HttpGet]
        [ProducesResponseType(typeof(CurrencyTransactionWebApi[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByDateRange(DateTime fromUtc, DateTime toUtc)
        {
            var transactions = await _currencyTransactionsServices.CurrencyTransactionService.GetByDateRangeAsync(fromUtc, toUtc);
            return Ok(transactions.Select(CurrencyTransactionWebApi.FromDomainModel).ToArray());
        }
        
        [Route("get-by-status/{status}")]
        [HttpGet]
        [ProducesResponseType(typeof(CurrencyTransactionWebApi[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByStatus(CurrencyTransactionStatus status)
        {
            var transactions = await _currencyTransactionsServices.CurrencyTransactionService.GetByStatusAsync(status);
            return Ok(transactions.Select(CurrencyTransactionWebApi.FromDomainModel).ToArray());
        }
    }
}