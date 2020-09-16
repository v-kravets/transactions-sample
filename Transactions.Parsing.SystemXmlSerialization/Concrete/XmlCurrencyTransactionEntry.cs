using System;
using System.Xml.Serialization;
using Transactions.Model.Concrete;

namespace Transactions.Parsing.SystemXmlSerialization.Concrete
{
    [XmlType("Transaction")]
    public class XmlCurrencyTransactionEntry
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("PaymentDetails")]
        public PaymentDetails PaymentDetails { get; set; }

        [XmlElement("TransactionDate")]
        public DateTime TransactionDateLocal { get; set; }
        
        [XmlElement("Status")]
        public XmlStatus XmlStatus { get; set; }

        public CurrencyTransaction ToCurrencyTransaction()
        {
            return CurrencyTransaction.Create(Id,
                PaymentDetails.Amount,
                PaymentDetails.CurrencyCode,
                TransactionDateLocal.ToUniversalTime(),
                XmlStatus.ToCurrencyTransactionStatus());
        }
    }

    public class PaymentDetails
    {
        [XmlElement("Amount")]
        public decimal Amount { get; set; }
        
        [XmlElement("CurrencyCode")]
        public string CurrencyCode { get; set; }
    }
}