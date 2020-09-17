using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transactions.Model.Concrete;
using Transactions.Services.Abstract;

namespace Transactions.Services.Transactions.Concrete
{
    public class CurrencyTransactionsService : ICurrencyTransactionService
    {
        private readonly ICurrencyTransactionRepository _currencyTransactionRepository;
        public CurrencyTransactionsService(ICurrencyTransactionRepository currencyTransactionRepository)
        {
            _currencyTransactionRepository = currencyTransactionRepository;
        }

        public Task SaveTransactionsAsync(IEnumerable<CurrencyTransaction> transaction)
            => _currencyTransactionRepository.SaveTransactionsAsync(transaction);

        public Task<IEnumerable<CurrencyTransaction>> GetByDateRangeAsync(DateTime fromUtc, DateTime toUtc)
            => _currencyTransactionRepository.GetByDateRangeAsync(fromUtc, toUtc);
        
        public Task<IEnumerable<CurrencyTransaction>> GetByCurrencyAsync(string currency) 
            => _currencyTransactionRepository.GetByCurrencyAsync(currency);
        
        public Task<IEnumerable<CurrencyTransaction>> GetByStatusAsync(CurrencyTransactionStatus status)
            => _currencyTransactionRepository.GetByStatusAsync(status);
    }
}