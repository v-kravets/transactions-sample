using Transactions.Model.Concrete;
using Transactions.Parsing.Abstract;

namespace Transactions.Parsing.TinyCsvParser.Concrete
{
    public class CsvParsingResult : IParsingResult
    {
        private CsvParsingResult() { }

        public CurrencyTransaction[] Transactions { get; private set; }
        public bool Success { get; private set; }
        public string Error { get; private set; }
        public CsvCurrencyTransactionRow[] CsvTransactions { get; set; }

        public static CsvParsingResult Create(CurrencyTransaction[] transaction, bool success, string error, CsvCurrencyTransactionRow[] csvTransactions)
        {
            return new CsvParsingResult()
            {
                Transactions = transaction,
                Success = success,
                Error = error,
                CsvTransactions = csvTransactions
            };
        }
    }
    
    
}