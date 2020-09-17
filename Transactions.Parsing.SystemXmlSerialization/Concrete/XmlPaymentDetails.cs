using System.Xml.Serialization;

namespace Transactions.Parsing.SystemXmlSerialization.Concrete
{
    public class XmlPaymentDetails
    {
        [XmlElement("Amount")]
        public string Amount { get; set; }
        
        [XmlElement("CurrencyCode")]
        public string CurrencyCode { get; set; }
    }
}