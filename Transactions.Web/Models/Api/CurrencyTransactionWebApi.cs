using Transactions.Model.Concrete;

namespace Transactions.Web.Models.Api
{
    public class CurrencyTransactionWebApi
    {
        public string Id { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }
        
        public static CurrencyTransactionWebApi FromDomainModel(CurrencyTransaction currencyTransaction)
        {
            return new CurrencyTransactionWebApi()
            {
                Id = currencyTransaction.Id,
                Payment = $"{currencyTransaction.Amount:.00} {currencyTransaction.CurrencyCode}",
                Status = currencyTransaction.Status.ToString()
            };
        }
    }
}