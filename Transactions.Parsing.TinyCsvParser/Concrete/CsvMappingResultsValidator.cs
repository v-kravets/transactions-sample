using System;
using System.Diagnostics;
using System.Linq;
using TinyCsvParser.Mapping;
using Transactions.Parsing.Abstract;
using Transactions.Parsing.Concrete;

namespace Transactions.Parsing.TinyCsvParser.Concrete
{
    internal class CsvMappingResultsValidator
    {
        public static IParsingResult GetPostParsingValidationResult(CsvMappingResult<CsvCurrencyTransactionRow>[] csvMappingResults)
        {
            if (csvMappingResults.Any(r => !r.IsValid))
            {
                return CsvParsingResult.Create(null,
                    false,
                    csvMappingResults.InvalidRowsError(),
                    csvMappingResults.Select(x => x.Result).ToArray()
                );
            }

            var idAttributesOver50Characters = csvMappingResults.Where(r => r.Result.Id.Length > 50).Select(t => t.Result.Id).ToArray();
            if (idAttributesOver50Characters.Length > 0)
            {
                return GetCsvParsingResultWithError($"Id over 50 found: {string.Join(", ", idAttributesOver50Characters)}", csvMappingResults);
            }

            var missedCurrencyCodeIds = csvMappingResults.Where(r => !ISO4217.CurrenctyCodes.Contains(r.Result.CurrencyCode))
                .Select(t => t.Result.Id)
                .ToArray();
            if (missedCurrencyCodeIds.Length > 0)
            {
                return GetCsvParsingResultWithError(
                    $"Invalid currency code for transactions with ids found: {string.Join(", ", missedCurrencyCodeIds)}",
                    csvMappingResults);
            }

            return CsvParsingResult.Create(
                csvMappingResults.Select(x => x.Result.ToCurrencyTransaction()).ToArray(),
                true,
                null,
                csvMappingResults.Select(x => x.Result).ToArray()
            );
        }

        private static CsvParsingResult GetCsvParsingResultWithError(string errorMessage, CsvMappingResult<CsvCurrencyTransactionRow>[] csvMappingResults)
        {
            Trace.TraceError(errorMessage);
            return CsvParsingResult.Create(null, false, errorMessage, csvMappingResults.Select(s => s.Result).ToArray());
        }
    }
    
    public static class ParsedRowsToStringExtension
    {
        public static string InvalidRowsError(this CsvMappingResult<CsvCurrencyTransactionRow>[] parsingResult)
        {
            return string.Join(Environment.NewLine, parsingResult.Where(x => !x.IsValid).Select(x => x.Error.ToString()));
        }
        
    }
}