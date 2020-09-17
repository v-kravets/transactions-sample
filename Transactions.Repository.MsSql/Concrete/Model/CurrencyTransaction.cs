using System;
using Transactions.Model.Concrete;

namespace Transactions.Repository.MsSql.Concrete.Model
{
    public partial class CurrencyTransaction
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public byte CurrencyId { get; set; }
        public DateTime TimestampUtc { get; set; }
        public byte StatusId { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Status Status { get; set; }
        
        public static CurrencyTransaction Create(string id, decimal amount, byte currencyId, DateTime dateUtc, byte statusId)
        {
            return new CurrencyTransaction
            {
                Id = id,
                Amount = amount,
                CurrencyId = currencyId,
                TimestampUtc = dateUtc,
                StatusId = statusId
            };
        }

        public Transactions.Model.Concrete.CurrencyTransaction ToDomainModel()
        {
            return Transactions.Model.Concrete.CurrencyTransaction.Create(Id,
                Amount,
                Currency.Name,
                TimestampUtc,
                (CurrencyTransactionStatus) StatusId);
        }
    }
}
