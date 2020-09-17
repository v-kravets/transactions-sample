using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Transactions.Model.Concrete;
using Transactions.Parsing.Concrete;

namespace Transactions.Parsing.SystemXmlSerialization.Concrete
{
    [XmlType("Transaction")]
    public class XmlCurrencyTransactionEntry
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("PaymentDetails")]
        public XmlPaymentDetails PaymentDetails { get; set; }

        [XmlElement("TransactionDate")]
        public string TransactionDateLocalString { get; set; }
        
        [XmlElement("Status")]
        public string Status { get; set; }

        [XmlIgnore]
        public decimal Amount { get; private set; }
        
        [XmlIgnore]
        public DateTime TransactionDateLocal { get; private set; }
        
        [XmlIgnore]
        public XmlStatus XmlStatus { get; private set; }        
        
        [XmlIgnore]
        public bool IsValid => Errors.Count == 0;

        [XmlIgnore]
        public int Index { get; set; }
        
        [XmlIgnore]
        public List<string> Errors { get; private set; } = new List<string>();
        
        [XmlIgnore]
        public string SummaryError => $"Object {Index} validation errors: " + string.Join(", ", Errors);

        public void Validate(int index)
        {
            Index = index;
            
            if (Id.Length > 50)
            {
                Errors.Add($"Id attributes is over 50 characters long: {Id}");
            }

            if (PaymentDetails == null)
            {
                Errors.Add($"Payment details part missed for for transaction");
            }
            else
            {
                if (decimal.TryParse(PaymentDetails.Amount, out var amount))
                {
                    Amount = amount;
                }
                else
                {
                    Errors.Add("Amount is not in correct decimal format");
                }
                
                if (!ISO4217.CurrenctyCodes.Contains(PaymentDetails.CurrencyCode.ToUpper()))
                {
                    Errors.Add($"Invalid currency code {PaymentDetails.CurrencyCode}");
                }
            }

            if (DateTime.TryParse(TransactionDateLocalString, out var dateTimeLocal))
            {
                TransactionDateLocal = dateTimeLocal;
            }
            else
            {
                Errors.Add("DateTime is in incorrect format");
            }
            
            if (Enum.TryParse<XmlStatus>(Status, out var status))
            {
                XmlStatus = status;
            }
            else
            {
                Errors.Add($"Invalid status: {Status}");
            }
        }
        
        public CurrencyTransaction ToCurrencyTransaction()
        {
            return
                IsValid
                    ? CurrencyTransaction.Create(Id,
                        Amount,
                        PaymentDetails.CurrencyCode,
                        DateTime.Parse(TransactionDateLocalString).ToUniversalTime(),
                        XmlStatus.ToCurrencyTransactionStatus())
                    : null;
        }

    }
}