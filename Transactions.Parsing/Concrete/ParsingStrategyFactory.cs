using System;
using Transactions.Parsing.Abstract;

namespace Transactions.Parsing.Concrete
{
    public class ParsingStrategyFactory : IParsingStrategyFactory
    {
        private readonly IParsingStrategy _csvParsingStrategy;
        private readonly IParsingStrategy _xmlParsingStrategy;

        public ParsingStrategyFactory(IParsingStrategy csvParsingStrategy, IParsingStrategy xmlParsingStrategy)
        {
            _csvParsingStrategy = csvParsingStrategy;
            _xmlParsingStrategy = xmlParsingStrategy;
        }

        public IParsingStrategy GetStrategy(string fileExtension)
        {
            if (fileExtension == null)
            {
                return null;
            }

            if (fileExtension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                return _csvParsingStrategy;
            }
            
            if (fileExtension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
            {
                return _xmlParsingStrategy;
            }
            
            return null;
        }
    }
}