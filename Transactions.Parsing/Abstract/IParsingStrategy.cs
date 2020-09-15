using System.IO;
using Transactions.Model.Concrete;

namespace Transactions.Parsing.Abstract
{
    public interface IParsingStrategy
    {
        IParsingResult ParseTransactions(Stream stream);
    }

    public interface IParsingResult
    {
        CurrencyTransaction[] Transactions { get; }
        bool Success { get; }
        string[] Errors { get; }
    }
}