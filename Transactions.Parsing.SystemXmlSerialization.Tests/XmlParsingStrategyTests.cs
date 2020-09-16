using System;
using System.IO;
using System.Linq;
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

        [Fact]
        public async Task ParsingOk()
        {
            var strategy = new XmlParsingStrategy();
            var results = await strategy.ParseTransactionsAsync(File.OpenRead("Resources\\Sample1.xml"));
            Assert.True(results.Success);
            Assert.Equal(2, results.Transactions.Length);
            
            _outputHelper.WriteLine(string.Join(Environment.NewLine, results.Transactions.Select(t => t.ToString())));
        }
    }
}
