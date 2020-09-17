using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transactions.Parsing.SystemXmlSerialization.Concrete;
using Xunit;
using Xunit.Abstractions;

namespace Transactions.Parsing.SystemXmlSerialization.Tests
{
    public class XmlParsingStrategyTests
    {
        private readonly ITestOutputHelper _outputHelper;
        public XmlParsingStrategyTests(ITestOutputHelper outputHelper) { _outputHelper = outputHelper; }

        [Theory]
        [InlineData(@"
<Transactions>
    <Transaction id='Inv00001'>
        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
        <PaymentDetails>
            <Amount>200.00</Amount>
            <CurrencyCode>USD</CurrencyCode>
        </PaymentDetails>
        <Status>Done</Status>
    </Transaction>
    <Transaction id='Inv00002'>
        <TransactionDate>2019-01-24T16:09:15</TransactionDate>
        <PaymentDetails>
            <Amount>10000.00</Amount>
            <CurrencyCode>EUR</CurrencyCode>
        </PaymentDetails>
        <Status>Rejected</Status>
    </Transaction>
</Transactions>
")]
        public async Task ParsedFewLisnesOk(string inputData)
        {
            var strategy = new XmlParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as XmlParsingResult;
            Assert.True(results.Success);
            Assert.Equal(2, results.XmlTransactions.Length);

            _outputHelper.WriteLine(string.Join(Environment.NewLine, results.XmlTransactions.Select(t => t.ToString())));
        }

        [Theory]
        [InlineData(@"
<Transactions>
    <Transaction id='Inv00001'>
        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
        <PaymentDetails>
            <Amount>200.00</Amount>
            <CurrencyCode>USD</CurrencyCode>
        </PaymentDetails>
        <Status>Done</Status>
    </Transaction>
</Transactions>
")]
        public async Task FieldDetailsReadOk(string inputData)
        {
            var strategy = new XmlParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as XmlParsingResult;
            Assert.True(results.Success);
            Assert.Single(results.XmlTransactions);
            Assert.Equal("Inv00001", results.XmlTransactions[0].Id);
            Assert.Equal(200.00M, results.XmlTransactions[0].Amount);
            Assert.Equal("USD", results.XmlTransactions[0].PaymentDetails.CurrencyCode);
            Assert.Equal(new DateTime(2019, 01, 23, 13, 45, 10), results.XmlTransactions[0].TransactionDateLocal);
            Assert.Equal(XmlStatus.Done, results.XmlTransactions[0].XmlStatus);
            
            _outputHelper.WriteLine(string.Join(Environment.NewLine, results.XmlTransactions.Select(t => t.ToString())));
        }
        
        [Theory]
        [InlineData(@"
<Transactions>
    <Transaction id='Invoice0000001Invoice0000001Invoice0000001Invoice0'>
        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
        <PaymentDetails>
            <Amount>200.00</Amount>
            <CurrencyCode>USD</CurrencyCode>
        </PaymentDetails>
        <Status>Done</Status>
    </Transaction>
</Transactions>
")]
        public async Task IdLengthFor50PassesOk(string inputData)
        {
            var strategy = new XmlParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as XmlParsingResult;
            Assert.True(results.Success);
            Assert.Single(results.XmlTransactions);
            
            _outputHelper.WriteLine(string.Join(Environment.NewLine, results.XmlTransactions.Select(t => t.ToString())));
        }
        
        [Theory]
        [InlineData(@"
<Transactions>
    <Transaction id='Invoice0000001Invoice0000001Invoice0000001Invoice01'>
        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
        <PaymentDetails>
            <Amount>200.00</Amount>
            <CurrencyCode>USD</CurrencyCode>
        </PaymentDetails>
        <Status>Done</Status>
    </Transaction>
    <Transaction id='Invoice0000001Invoice0000001Invoi'>
        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
        <PaymentDetails>
            <Amount>200.00</Amount>
            <CurrencyCode>USD</CurrencyCode>
        </PaymentDetails>
        <Status>Done</Status>
    </Transaction>
    <Transaction id='Invoice0000001Invoice0000001Invoice0000001Invoice01'>
        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
        <PaymentDetails>
            <Amount>200.00</Amount>
            <CurrencyCode>USD</CurrencyCode>
        </PaymentDetails>
        <Status>Done</Status>
    </Transaction>
</Transactions>
")]
        public async Task IdLengthFor51Fails(string inputData)
        {
            var strategy = new XmlParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as XmlParsingResult;
            Assert.False(results.Success);
            Assert.Equal($"Object 1 validation errors: Id attributes is over 50 characters long: Invoice0000001Invoice0000001Invoice0000001Invoice01\r\nObject 3 validation errors: Id attributes is over 50 characters long: Invoice0000001Invoice0000001Invoice0000001Invoice01", results.Error);
            
            _outputHelper.WriteLine(string.Join(Environment.NewLine, results.XmlTransactions.Select(t => t.ToString())));
        }
        
        [Theory]
        [InlineData(@"
<Transactions>
    <Transaction id='Invoice0000001'>
        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
        <PaymentDetails>
            <Amount>200.00</Amount>
            <CurrencyCode>ZZZ</CurrencyCode>
        </PaymentDetails>
        <Status>Done</Status>
    </Transaction>
</Transactions>
")]
        public async Task InvalidCurrencyError(string inputData)
        {
            var strategy = new XmlParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as XmlParsingResult;
            Assert.False(results.Success);
            Assert.Equal($"Object 1 validation errors: Invalid currency code ZZZ", results.Error);
            
            _outputHelper.WriteLine(string.Join(Environment.NewLine, results.XmlTransactions.Select(t => t.ToString())));
        }
        
        [Theory]
        [InlineData(@"
<Transactions>
    <Transaction id='Invoice0000001'>
        <TransactionDate>2019-01-23T13:45:10</TransactionDate>
        <PaymentDetails>
            <Amount>200.00</Amount>
            <CurrencyCode>USD</CurrencyCode>
        </PaymentDetails>
        <Status>SomeStatus</Status>
    </Transaction>
</Transactions>
")]
        public async Task InvalidStatusError(string inputData)
        {
            var strategy = new XmlParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as XmlParsingResult;
            Assert.False(results.Success);

            Assert.Equal($"Object 1 validation errors: Invalid status: SomeStatus", results.Error);
        }
        
        [Theory]
        [InlineData(@"
<Transactions>
    <Transaction id='Invoice0000001'>
        <TransactionDate>InvalidDate/TransactionDate>
        <PaymentDetails>
            <Amount>200.00</Amount>
            <CurrencyCode>USD</CurrencyCode>
        </PaymentDetails>
        <Status>SomeStatus</Status>
    </Transaction>
</Transactions>
")]
        public async Task InvalidDateError(string inputData)
        {
            var strategy = new XmlParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as XmlParsingResult;
            Assert.False(results.Success);
            
            Assert.Equal($"There is an error in XML document (5, 10).", results.Error);
        }
        
        [Theory]
        [InlineData("")]
        public async Task EmptyStringPassedOk(string inputData)
        {
            var strategy = new XmlParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(new MemoryStream(Encoding.UTF8.GetBytes(inputData))) as XmlParsingResult;
            Assert.False(results.Success);
            Assert.Equal("There is an error in XML document (0, 0).", results.Error);
        }
        
        
    }
}
