using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

namespace Transactions.Parsing.TinyCsvParser.Concrete
{
    public class CsvCurrencyTransactionRowMapping : CsvMapping<CsvCurrencyTransactionRow>
    {
        public CsvCurrencyTransactionRowMapping()
        {
            MapProperty(0, x => x.Id);
            MapProperty(1, x => x.Amount);
            MapProperty(2, x => x.CurrencyCode);
            MapProperty(3, x => x.TransactionDateLocal, new DateTimeConverter("dd/MM/yyyy hh:mm:ss"));
            MapProperty(4, x => x.CsvStatus, new EnumConverter<CsvStatus>());
        }
    }
}