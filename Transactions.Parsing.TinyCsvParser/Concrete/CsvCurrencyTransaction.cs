using System;

namespace Transactions.Parsing.TinyCsvParser.Concrete
{
    public class CsvCurrencyTransaction
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDateLocal { get; set; }
        public CsvStatus CsvStatus { get; set; }
    }
}