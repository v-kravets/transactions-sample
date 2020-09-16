using System;

namespace Transactions.Model.Concrete
{
    public class CurrencyTransaction
    {
        
        public CurrencyTransaction() { }

        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDateUtc { get; set; }
        public CurrencyTransactionStatus Status { get; set; }

        public static CurrencyTransaction Create(string id, decimal amount, string currencyCode, DateTime transactionDateUtc, CurrencyTransactionStatus status)
        {
            return new CurrencyTransaction()
            {
                Id = id,
                Amount = amount,
                CurrencyCode = currencyCode,
                TransactionDateUtc = transactionDateUtc,
                Status = status
            };
        }

        public override string ToString()
        {
            return $"{Id} {Amount} {CurrencyCode} {TransactionDateUtc:o} {Status}";
        }
    }
}