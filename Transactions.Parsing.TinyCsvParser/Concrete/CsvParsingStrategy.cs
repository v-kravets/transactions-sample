using System.IO;
using System.Threading.Tasks;
using Transactions.Parsing.Abstract;
using Transactions.Parsing.TinyCsvParser.Abstract;

namespace Transactions.Parsing.TinyCsvParser.Concrete
{
    public class CsvParsingStrategy : ICsvParsingStrategy
    {
        public Task<IParsingResult> ParseTransactionsAsync(Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}