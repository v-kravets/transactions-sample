using System;

namespace Transactions.Model.Concrete
{
    public class CurrencyTransaction
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDateUtc { get; set; }
        public Status Status { get; set; }
    }
}