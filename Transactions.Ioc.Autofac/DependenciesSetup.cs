
using Autofac;
using Transactions.Parsing.Abstract;
using Transactions.Parsing.Concrete;
using Transactions.Parsing.SystemXmlSerialization.Abstract;
using Transactions.Parsing.SystemXmlSerialization.Concrete;
using Transactions.Parsing.TinyCsvParser.Abstract;
using Transactions.Parsing.TinyCsvParser.Concrete;
using Transactions.Repository.MsSql.Concrete.Model;
using Transactions.Services.Abstract;
using Transactions.Services.Concrete;
using Transactions.Services.Transactions.Concrete;

namespace Transactions.Ioc.Autofac
{
    public static class DependenciesSetup
    {
        public static ContainerBuilder SetupDependencies(this ContainerBuilder builder)
        {
            builder.RegisterType<TransactionsContext>().As<ICurrencyTransactionRepository>().InstancePerLifetimeScope();
            
            builder.RegisterType<CurrencyTransactionsService>().As<ICurrencyTransactionService>().InstancePerLifetimeScope();
            builder.RegisterType<CurrencyTransactionsServices>().As<ICurrencyTransactionsServices>().InstancePerLifetimeScope();
            
            builder.RegisterType<CsvParsingStrategy>().As<ICsvParsingStrategy>().SingleInstance();
            builder.RegisterType<XmlParsingStrategy>().As<IXmlParsingStrategy>().SingleInstance();
            
            builder.Register(c => new ParsingStrategyFactory(c.Resolve<ICsvParsingStrategy>(), c.Resolve<IXmlParsingStrategy>()))
                .As<IParsingStrategyFactory>()
                .SingleInstance();
            
            return builder;
        }
    }
}