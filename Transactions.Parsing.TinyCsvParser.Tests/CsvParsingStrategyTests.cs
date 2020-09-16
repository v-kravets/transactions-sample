using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Transactions.Parsing.TinyCsvParser.Concrete;
using Xunit;
using Xunit.Abstractions;

namespace Transactions.Parsing.TinyCsvParser.Tests
{
    public class CsvParsingStrategyTests
    {
        private readonly ITestOutputHelper _outputHelper;
        public CsvParsingStrategyTests(ITestOutputHelper outputHelper) { _outputHelper = outputHelper; }

        [Fact]
        public async Task ParsedOk()
        {
            var strategy = new CsvParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(File.OpenRead("Resources\\Sample1.csv"));
            Assert.True(results.Success);
            Assert.Equal(2, results.Transactions.Length);
            
            _outputHelper.WriteLine(string.Join(Environment.NewLine, results.Transactions.Select(t => t.ToString())));
        }
    }
}
