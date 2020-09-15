using System.IO;
using System.Threading.Tasks;

namespace Transactions.Parsing.Abstract
{
    public interface IParsingStrategy
    {
        Task<IParsingResult> ParseTransactionsAsync(Stream stream);
    }
}