using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transactions.Model.Concrete;

namespace Transactions.Services.Abstract
{
    public interface ICurrencyTransactionService
    {
        Task SaveTransactionsAsync(IEnumerable<CurrencyTransaction> transaction);
        Task<IEnumerable<CurrencyTransaction>> GetByDateRangeAsync(DateTime fromUtc, DateTime toUtc);
        Task<IEnumerable<CurrencyTransaction>> GetByCurrencyAsync(string currency);
        Task<IEnumerable<CurrencyTransaction>> GetByStatusAsync(CurrencyTransactionStatus status);
    }
}