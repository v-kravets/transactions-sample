using Transactions.Model.Concrete;

namespace Transactions.Parsing.Abstract
{
    public interface IParsingResult
    {
        CurrencyTransaction[] Transactions { get; }
        bool Success { get; }
        string Error { get; }
    }
}