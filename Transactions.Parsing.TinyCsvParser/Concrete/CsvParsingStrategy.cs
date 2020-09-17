using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinyCsvParser;
using Transactions.Parsing.Abstract;
using Transactions.Parsing.TinyCsvParser.Abstract;

namespace Transactions.Parsing.TinyCsvParser.Concrete
{
    public class CsvParsingStrategy : ICsvParsingStrategy
    {
        public Task<IParsingResult> ParseTransactionsAsync(Stream stream)
        {
            var lines = new StreamReader(stream).ReadToEnd();
            lines = lines.Replace("�", "\"");
            var parserOptions = new CsvParserOptions(false, ',');
            var csvParser = new CsvParser<CsvCurrencyTransactionRow>(parserOptions, new CsvCurrencyTransactionRowMapping());
            var parsingResult = csvParser
                .ReadFromString(new CsvReaderOptions(new[] {Environment.NewLine}), lines)
                .Select(r => new CsvMappingResultWrapper(r))
                .ToArray()
                .Validate();
            return Task.FromResult(CsvMappingResultsValidator.GetPostParsingValidationResult(parsingResult));
        }
        
    }
}