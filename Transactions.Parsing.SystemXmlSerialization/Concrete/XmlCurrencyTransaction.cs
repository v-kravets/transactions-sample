using System;

namespace Transactions.Parsing.SystemXmlSerialization.Concrete
{
    public class XmlCurrencyTransaction
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDateLocal { get; set; }
        public XmlStatus XmlStatus { get; set; }
    }
}