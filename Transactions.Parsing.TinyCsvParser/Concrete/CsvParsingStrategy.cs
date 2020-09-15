using System.IO;
using Transactions.Parsing.Abstract;
using Transactions.Parsing.TinyCsvParser.Abstract;

namespace Transactions.Parsing.TinyCsvParser.Concrete
{
    public class CsvParsingStrategy : ICsvParsingStrategy
    {
        public IParsingResult ParseTransactions(Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}