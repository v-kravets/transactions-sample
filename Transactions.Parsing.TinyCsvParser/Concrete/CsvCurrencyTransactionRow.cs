using System;
using System.Collections.Generic;
using TinyCsvParser.Mapping;
using Transactions.Model.Concrete;
using Transactions.Parsing.Concrete;

namespace Transactions.Parsing.TinyCsvParser.Concrete
{
    public class CsvCurrencyTransactionRow
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDateLocal { get; set; }
        public CsvStatus CsvStatus { get; set; }

        public CurrencyTransaction ToCurrencyTransaction()
        {
            return CurrencyTransaction.Create(Id,
                Amount,
                CurrencyCode,
                TransactionDateLocal.ToUniversalTime(),
                CsvStatus.ToCurrencyTransactionStatus());
        }
    }

    public class CsvMappingResultWrapper
    {
        private CsvMappingResult<CsvCurrencyTransactionRow> row;
        public CsvMappingResultWrapper(CsvMappingResult<CsvCurrencyTransactionRow> row) { this.row = row; }

        public CsvCurrencyTransactionRow CsvCurrencyTransactionRow => row.Result;
            
        public bool IsValid => Errors.Count == 0;
        public int Index { get; set; }
        public List<string> Errors { get; private set; } = new List<string>();
        public string SummaryError => $"Row {Index} validation errors: " + string.Join(", ", Errors);
        
        public void Validate(int index)
        {
            Index = index;
            
            if (!row.IsValid)
            {
                Errors.Add(row.Error.ToString());
                return;
            }
            
            if(row.Result.Id.Length > 50)
            {
                Errors.Add("Id field is over 50 characters");
            }

            if (!ISO4217.CurrenctyCodes.Contains(row.Result.CurrencyCode.ToUpper()))
            {
                Errors.Add($"Invalid currency code {row.Result.CurrencyCode}");
            }
        }
    }
    
}