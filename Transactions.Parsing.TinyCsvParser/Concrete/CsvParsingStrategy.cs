using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            lines = Regex.Replace(lines, @"”\s*,\s*[”“]+", "\t");
            lines = Regex.Replace(lines, @"[”“]", "");
            var parserOptions = new CsvParserOptions(false, '\t');
            var csvParser = new CsvParser<CsvCurrencyTransactionRow>(parserOptions, new CsvCurrencyTransactionRowMapping());
            var parsingResult = csvParser.ReadFromString(new CsvReaderOptions(new [] {Environment.NewLine}), lines).ToArray();
            return Task.FromResult(CsvMappingResultsValidator.GetPostParsingValidationResult(parsingResult));
        }
        
    }
}