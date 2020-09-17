using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Transactions.Model.Concrete;
using Transactions.Services.Abstract;

namespace Transactions.Repository.MsSql.Concrete.Model
{
    public partial class TransactionsContext : DbContext, ICurrencyTransactionRepository
    {
        public async Task SaveTransactionsAsync(IEnumerable<Transactions.Model.Concrete.CurrencyTransaction> transactions)
        {
            var currenciesDict = Currency.ToDictionary(d => d.Name, d => d.Id);
            var transactionsToSave = new List<CurrencyTransaction>();
            foreach (var currencyTransaction in transactions)
            {
                transactionsToSave.Add(Concrete.Model.CurrencyTransaction.Create(currencyTransaction.Id,
                    currencyTransaction.Amount,
                    currenciesDict[currencyTransaction.CurrencyCode],
                    currencyTransaction.TransactionDateUtc,
                    (byte) currencyTransaction.Status));
            }

            await CurrencyTransaction.AddRangeAsync(transactionsToSave);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Transactions.Model.Concrete.CurrencyTransaction>> GetByDateRangeAsync(DateTime fromUtc, DateTime toUtc)
        {
            var items = await CurrencyTransaction.Where(t => fromUtc <= t.TimestampUtc && t.TimestampUtc <= toUtc).ToListAsync();
            return items.Select(t => t.ToDomainModel()).ToList();
        }

        public async Task<IEnumerable<Transactions.Model.Concrete.CurrencyTransaction>> GetByCurrencyAsync(string currency)
        {
            var items = await CurrencyTransaction.Where(t => t.Currency.Name == currency.ToUpper()).ToListAsync();
            return items.Select(t => t.ToDomainModel()).ToList();
        }

        public async Task<IEnumerable<Transactions.Model.Concrete.CurrencyTransaction>> GetByStatusAsync(CurrencyTransactionStatus status)
        {
            var items = await CurrencyTransaction.Where(t => t.StatusId == (byte) status).ToListAsync();
            return items.Select(t => t.ToDomainModel()).ToList();
        }
    }
}