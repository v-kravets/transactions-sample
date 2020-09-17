using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Transactions.Parsing.Abstract;
using Transactions.Parsing.SystemXmlSerialization.Abstract;
using Transactions.Utils;

namespace Transactions.Parsing.SystemXmlSerialization.Concrete
{
    public class XmlParsingStrategy : IXmlParsingStrategy
    {
        public async Task<IParsingResult> ParseTransactionsAsync(Stream stream)
        {
            try
            {
                using (var sr = new StreamReader(stream))
                {
                    var stringToDeserialize = await sr.ReadToEndAsync();
                    var xmlTransactions = DeserializeFromXmlStringAsync(stringToDeserialize).Validate();
                    return XmlParsingStrategyEntryValidator.GetPostParsingValidationResult(xmlTransactions);
                }
            }
            catch (InvalidOperationException ex)
            {
                Trace.TraceError(ex.ExpandException(), ex);
                return XmlParsingResult.Create(false, ex.Message, null);
            }
        }

        private static XmlCurrencyTransactionEntry[] DeserializeFromXmlStringAsync(string objectToDeserialize)
        {
            using (TextReader reader = new StringReader(objectToDeserialize))
            {
                var serializer = new XmlSerializer(typeof(XmlCurrencyTransactionEntry[]), new XmlRootAttribute("Transactions"));
                return (XmlCurrencyTransactionEntry[])serializer.Deserialize(reader);
            }
        }
    }
    
}