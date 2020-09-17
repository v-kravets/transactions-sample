using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transactions.Model.Concrete;
using Transactions.Parsing.TinyCsvParser.Concrete;
using Xunit;
using Xunit.Abstractions;

namespace Transactions.Parsing.TinyCsvParser.Tests
{
    public class CsvParsingStrategyTests
    {
        private readonly ITestOutputHelper _outputHelper;
        public CsvParsingStrategyTests(ITestOutputHelper outputHelper) { _outputHelper = outputHelper; }

        [Theory]
        [InlineData("\"Invoice0000001\",\"1,000.00\", \"USD\", \"20/02/2019 12:33:16\", \"Approved\"\r\n"
                    + "\"Invoice0000002\",\"300.00\",\"USD\",\"21/02/2019 02:04:59\", \"Failed\"")]
        public async Task ParsedFewLisnesOk(string inputData)
        {
            var strategy = new CsvParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as CsvParsingResult;
            Assert.True(results.Success);
            Assert.Equal(2, results.Transactions.Length);
            
            _outputHelper.WriteLine(string.Join(Environment.NewLine, results.Transactions.Select(t => t.ToString())));
        }
        
        [Theory]
        [InlineData("\"Invoice0000001\",\"1,000.00\", \"USD\", \"20/02/2019 12:33:16\", \"Approved\"")]
        public async Task FieldDetailsReadOk(string inputData)
        {
            var strategy = new CsvParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as CsvParsingResult;
            Assert.True(results.Success);
            Assert.Single(results.Transactions);
            Assert.Equal("Invoice0000001", results.Transactions[0].Id);
            Assert.Equal(1000.00M, results.Transactions[0].Amount);
            Assert.Equal("USD", results.Transactions[0].CurrencyCode);
            Assert.Equal(new DateTime(2019, 02, 20, 00, 33, 16).ToUniversalTime(), results.Transactions[0].TransactionDateUtc);
            Assert.Equal(CurrencyTransactionStatus.A, results.Transactions[0].Status);
            
            _outputHelper.WriteLine(string.Join(Environment.NewLine, results.Transactions.Select(t => t.ToString())));
        }
        
        [Theory]
        [InlineData("\"Invoice0000001Invoice0000001Invoice0000001Invoice0\",\"1,000.00\", \"USD\", \"20/02/2019 12:33:16\", \"Approved\"")]
        public async Task IdLengthFor50PassesOk(string inputData)
        {
            var strategy = new CsvParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as CsvParsingResult;
            Assert.True(results.Success);
            Assert.Single(results.Transactions);
            
            _outputHelper.WriteLine(string.Join(Environment.NewLine, results.Transactions.Select(t => t.ToString())));
        }
        
        [Theory]
        [InlineData("\"Invoice0000001Invoice0000001Invoice0000001Invoice01\",\"1,000.00\", \"USD\", \"20/02/2019 12:33:16\", \"Approved\"\r\n"
                    + "\"Invoice0000001Invoice0000001Invoice0000001Invoice011\",\"1,000.00\", \"USD\", \"20/02/2019 12:33:16\", \"Approved\"")]
        public async Task IdLengthFor51Fails(string inputData)
        {
            var strategy = new CsvParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as CsvParsingResult;
            Assert.False(results.Success);
            Assert.Equal($"Row 1 validation errors: Id field is over 50 characters\r\nRow 2 validation errors: Id field is over 50 characters", results.Error);
            
            _outputHelper.WriteLine(string.Join(Environment.NewLine, ((CsvParsingResult)results).CsvTransactions.Select(t => t.ToString())));
        }
        
        [Theory]
        [InlineData("\"Invoice0000001\",\"1,000.00\", \"ZZZ\", \"20/02/2019 12:33:16\", \"Approved\"")]
        public async Task InvalidCurrencyError(string inputData)
        {
            var strategy = new CsvParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as CsvParsingResult;
            Assert.False(results.Success);
            
            Assert.Equal($"Row 1 validation errors: Invalid currency code ZZZ", results.Error);
            
            _outputHelper.WriteLine(string.Join(Environment.NewLine, ((CsvParsingResult)results).CsvTransactions.Select(t => t.ToString())));
        }
        
        [Theory]
        [InlineData("\"Invoice0000001\",\"1,000.00\", \"ZZZ\", \"20/02/2019 12:33:16\", \"SomeStatus\"")]
        public async Task InvalidStatusError(string inputData)
        {
            var strategy = new CsvParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as CsvParsingResult;
            Assert.False(results.Success);
            
            Assert.Equal($"Row 1 validation errors: CsvMappingError (ColumnIndex = 4, Value = Column 4 with Value 'SomeStatus' cannot be converted, UnmappedRow = Invoice0000001|1,000.00|ZZZ|20/02/2019 12:33:16|SomeStatus)", results.Error);
        }
        
        [Theory]
        [InlineData("\"Invoice0000001\",\"1,000.00\", \"ZZZ\", \"InvalidData\", \"Approved\"")]
        public async Task InvalidDateError(string inputData)
        {
            var strategy = new CsvParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as CsvParsingResult;
            Assert.False(results.Success);
            
            Assert.Equal($"Row 1 validation errors: CsvMappingError (ColumnIndex = 3, Value = Column 3 with Value 'InvalidData' cannot be converted, UnmappedRow = Invoice0000001|1,000.00|ZZZ|InvalidData|Approved)", results.Error);
        }
        
        [Theory]
        [InlineData("")]
        public async Task EmptyStringPassedOk(string inputData)
        {
            var strategy = new CsvParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as CsvParsingResult;
            Assert.True(results.Success);
            Assert.Empty(results.Transactions);
        }
        
        
    }
}
