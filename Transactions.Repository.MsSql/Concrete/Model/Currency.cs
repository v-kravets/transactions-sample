using System.Collections.Generic;

namespace Transactions.Repository.MsSql.Concrete.Model
{
    public partial class Currency
    {
        public Currency()
        {
            CurrencyTransaction = new HashSet<CurrencyTransaction>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CurrencyTransaction> CurrencyTransaction { get; set; }
    }
}
