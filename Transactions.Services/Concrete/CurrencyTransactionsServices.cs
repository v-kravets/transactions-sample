using Transactions.Services.Abstract;

namespace Transactions.Services.Concrete
{
    public class CurrencyTransactionsServices : ICurrencyTransactionsServices
    {
        public CurrencyTransactionsServices(ICurrencyTransactionService currencyTransactionService)
        {
            CurrencyTransactionService = currencyTransactionService;
        }
        
        public ICurrencyTransactionService CurrencyTransactionService { get; set; }
    }
}