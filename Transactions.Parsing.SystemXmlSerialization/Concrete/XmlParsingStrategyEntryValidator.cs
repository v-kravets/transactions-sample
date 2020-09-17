using System.Diagnostics;
using System.Linq;
using Transactions.Parsing.Abstract;

namespace Transactions.Parsing.SystemXmlSerialization.Concrete
{
    internal static class XmlParsingStrategyEntryValidator
    {
        public static IParsingResult GetPostParsingValidationResult(XmlCurrencyTransactionEntry[] records)
        {
            if (records.All(r => r.IsValid))
            {
                return XmlParsingResult.Create(true, null, records);
            }

            var errors = string.Join("\r\n", records.Where(r => !r.IsValid).Select(r => r.SummaryError));
            Trace.TraceError(errors);
            return XmlParsingResult.Create(false, errors, records);
        }

        public static XmlCurrencyTransactionEntry[] Validate(this XmlCurrencyTransactionEntry[] xmlCurrencyTransactionEntries)
        {
            var index = 1;
            foreach (var xmlCurrencyTransactionEntry in xmlCurrencyTransactionEntries)
            {
                xmlCurrencyTransactionEntry.Validate(index++);
            }

            return xmlCurrencyTransactionEntries;
        }
    }
}