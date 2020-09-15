using System;
using Transactions.Model.Concrete;

namespace Transactions.Parsing.SystemXmlSerialization.Concrete
{
    public class XmlCurrencyTransactionEntry
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDateLocal { get; set; }
        public XmlStatus XmlStatus { get; set; }

        public CurrencyTransaction ToCurrencyTransaction()
        {
            return CurrencyTransaction.Create(Id,
                Amount,
                CurrencyCode,
                TransactionDateLocal.ToUniversalTime(),
                XmlStatus.ToCurrencyTransactionStatus());
        }
    }
}