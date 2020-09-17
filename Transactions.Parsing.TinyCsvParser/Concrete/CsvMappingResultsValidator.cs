using System;
using System.Diagnostics;
using System.Linq;
using TinyCsvParser.Mapping;
using Transactions.Parsing.Abstract;

namespace Transactions.Parsing.TinyCsvParser.Concrete
{
    internal static class CsvMappingResultsValidator
    {
        public static IParsingResult GetPostParsingValidationResult(CsvMappingResultWrapper[] records)
        {
            if (records.All(r => r.IsValid))
            {
                return CsvParsingResult.Create(true, null, records);
            }
            
            var errors = string.Join("\r\n", records.Where(r => !r.IsValid).Select(r => r.SummaryError));
            Trace.TraceError(errors);
            return CsvParsingResult.Create(false, errors, records);
        }

        public static CsvMappingResultWrapper[] Validate(this CsvMappingResultWrapper[] csvMappingResultWrapperObjects)
        {
            var index = 1;
            foreach (var xmlCurrencyTransactionEntry in csvMappingResultWrapperObjects)
            {
                xmlCurrencyTransactionEntry.Validate(index++);
            }

            return csvMappingResultWrapperObjects;
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