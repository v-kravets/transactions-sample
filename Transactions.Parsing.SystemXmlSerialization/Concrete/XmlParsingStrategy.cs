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
                    var xmlTransactions = await DeserializeFromXmlStringAsync<XmlCurrencyTransactionEntry[]>(stringToDeserialize);
                    return XmlParsingResult.Create(true, null, xmlTransactions);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceInformation(ex.ExpandException());
                return XmlParsingResult.Create(false, ex.Message, null);
            }
        }
        
        private static async Task<T> DeserializeFromXmlStringAsync<T>(string objectToDeserialize)
        {
            using (TextReader reader = new StringReader(objectToDeserialize))
            {
                var serializer = new XmlSerializer(typeof(T));
                return await Task.FromResult((T)serializer.Deserialize(reader));
            }
        }
    }
    
    
}