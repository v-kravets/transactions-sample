using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transactions.Model.Concrete;

namespace Transactions.Services.Abstract
{
    public interface ICurrencyTransactionRepository
    {
        Task SaveTransactionsAsync(IEnumerable<CurrencyTransaction> transactions);
        Task<IEnumerable<CurrencyTransaction>> GetByDateRangeAsync(DateTime fromUtc, DateTime toUtc);
        Task<IEnumerable<CurrencyTransaction>> GetByCurrencyAsync(string currency);
        Task<IEnumerable<CurrencyTransaction>> GetByStatusAsync(CurrencyTransactionStatus status);
    }
}