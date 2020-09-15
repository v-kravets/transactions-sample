using System;
using Transactions.Model.Concrete;

namespace Transactions.Parsing.SystemXmlSerialization.Concrete
{
    public enum XmlStatus
    {
        Approved,
        Rejected,
        Done
    }

    public static class XmlStatusExtension
    {
        public static CurrencyTransactionStatus ToCurrencyTransactionStatus(this XmlStatus status)
        {
            switch (status)
            {
                case XmlStatus.Approved:
                    return CurrencyTransactionStatus.A;
                case XmlStatus.Rejected:
                    return CurrencyTransactionStatus.R;
                case XmlStatus.Done:
                    return CurrencyTransactionStatus.D;
                default:
                    throw new InvalidOperationException($"Can not identify currency transaction status for xml status: {status}");
            }
        }
    }
}