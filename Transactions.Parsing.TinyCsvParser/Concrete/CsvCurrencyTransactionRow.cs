using System;
using Transactions.Model.Concrete;

namespace Transactions.Parsing.TinyCsvParser.Concrete
{
    public class CsvCurrencyTransactionRow
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDateLocal { get; set; }
        public CsvStatus CsvStatus { get; set; }

        public CurrencyTransaction ToCurrencyTransaction()
        {
            return CurrencyTransaction.Create(Id,
                Amount,
                CurrencyCode,
                TransactionDateLocal.ToUniversalTime(),
                CsvStatus.ToCurrencyTransactionStatus());
        }
    }
    
    
}