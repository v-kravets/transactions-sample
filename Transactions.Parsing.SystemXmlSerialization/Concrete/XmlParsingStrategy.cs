using System.IO;
using Transactions.Parsing.Abstract;
using Transactions.Parsing.SystemXmlSerialization.Abstract;

namespace Transactions.Parsing.SystemXmlSerialization.Concrete
{
    public class XmlParsingStrategy : IXmlParsingStrategy
    {
        public IParsingResult ParseTransactions(Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}