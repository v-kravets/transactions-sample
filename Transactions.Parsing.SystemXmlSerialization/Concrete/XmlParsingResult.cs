using System.Linq;
using Transactions.Model.Concrete;
using Transactions.Parsing.Abstract;

namespace Transactions.Parsing.SystemXmlSerialization.Concrete
{
    public class XmlParsingResult : IParsingResult
    {
        private XmlParsingResult() { }

        public CurrencyTransaction[] Transactions { get; private set; }
        public bool Success { get; private set; }
        public string Error { get; private set; }
        public XmlCurrencyTransactionEntry[] XmlTransactions { get; set; }

        public static XmlParsingResult Create(
            bool success,
            string error,
            XmlCurrencyTransactionEntry[] xmlTransactions)
        {
            return new XmlParsingResult()
            {
                Transactions = success ? xmlTransactions.Select(x => x.ToCurrencyTransaction()).ToArray() : null,
                Success = success,
                Error = error,
                XmlTransactions = xmlTransactions
            };
        }
    }
}