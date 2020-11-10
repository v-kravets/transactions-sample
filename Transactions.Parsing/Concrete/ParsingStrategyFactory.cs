using System;
using Transactions.Parsing.Abstract;

namespace Transactions.Parsing.Concrete
{
    public class ParsingStrategyFactory : IParsingStrategyFactory
    {
        private readonly ICsvParsingStrategy _csvParsingStrategy;
        private readonly IXmlParsingStrategy _xmlParsingStrategy;

        public ParsingStrategyFactory(ICsvParsingStrategy csvParsingStrategy, IXmlParsingStrategy xmlParsingStrategy)
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