using System.Diagnostics;
using System.Linq;
using Transactions.Parsing.Abstract;
using Transactions.Parsing.Concrete;

namespace Transactions.Parsing.SystemXmlSerialization.Concrete
{
    internal class XmlParsingStrategyEntryValidator
    {
        public static IParsingResult GetPostParsingValidationResult(XmlCurrencyTransactionEntry[] records)
        {
            var idAttributesOver50Characters = records.Where(r => r.Id.Length > 50).Select(t => t.Id).ToArray();
            if (idAttributesOver50Characters.Length > 0)
            {
                return GetXmlParsingResultWithError($"Ids attributes over 50 found: {string.Join(", ", idAttributesOver50Characters)}", records);
            }

            var missedPaymentDetailsIds = records.Where(r => r.PaymentDetails == null).Select(t => t.Id).ToArray();
            if (missedPaymentDetailsIds.Length > 0)
            {
                return GetXmlParsingResultWithError($"Payment details part missed for for transactions with ids: {string.Join(", ", missedPaymentDetailsIds)}", records);
            }

            var missedCurrencyCodeIds = records.Where(r => !ISO4217.CurrenctyCodes.Contains(r.PaymentDetails.CurrencyCode))
                .Select(t => t.Id)
                .ToArray();
            if (missedCurrencyCodeIds.Length > 0)
            {
                return GetXmlParsingResultWithError($"Invalid currency code for transactions with ids found: {string.Join(", ", missedCurrencyCodeIds)}", records);
            }

            return XmlParsingResult.Create(true, null, records);
        }
        
        private static XmlParsingResult GetXmlParsingResultWithError(string errorMessage, XmlCurrencyTransactionEntry[] xmlTransactions)
        {
            Trace.TraceError(errorMessage);
            return XmlParsingResult.Create(false, errorMessage, xmlTransactions);
        }
    }
}