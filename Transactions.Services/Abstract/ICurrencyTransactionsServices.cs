namespace Transactions.Services.Abstract
{
    public interface ICurrencyTransactionsServices
    {
        ICurrencyTransactionService CurrencyTransactionService { get; set; }
    }
}