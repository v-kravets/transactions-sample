using System;
using Transactions.Model.Concrete;

namespace Transactions.Parsing.TinyCsvParser.Concrete
{
    public enum CsvStatus
    {
        Approved,
        Failed,
        Finished
    }
    
    public static class CsvStatusExtension
    {
        public static CurrencyTransactionStatus ToCurrencyTransactionStatus(this CsvStatus status)
        {
            switch (status)
            {
                case CsvStatus.Approved:
                    return CurrencyTransactionStatus.A;
                case CsvStatus.Failed:
                    return CurrencyTransactionStatus.R;
                case CsvStatus.Finished:
                    return CurrencyTransactionStatus.D;
                default:
                    throw new InvalidOperationException($"Can not identify currency transaction status for csv status: {status}");
            }
        }
    }
}