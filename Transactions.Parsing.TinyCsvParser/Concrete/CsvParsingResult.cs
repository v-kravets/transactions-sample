using System.Linq;
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
        public CsvMappingResultWrapper[] CsvTransactions { get; set; }

        public static CsvParsingResult Create(bool success, string error, CsvMappingResultWrapper[] csvTransactions)
        {
            return new CsvParsingResult()
            {
                Transactions = success ? csvTransactions.Select(s => s.CsvCurrencyTransactionRow.ToCurrencyTransaction()).ToArray() : null,
                Success = success,
                Error = error,
                CsvTransactions = csvTransactions
            };
        }
    }
    
    
}