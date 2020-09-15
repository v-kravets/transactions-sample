namespace Transactions.Parsing.Abstract
{
    public interface IParsingStrategyFactory
    {
        IParsingStrategy GetStrategy(string fileExtension);
    }
}