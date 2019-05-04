using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace ExchangeRatesApi.Test
{
    public class RequestBuilderTest
    {
        [Theory]
        [ClassData(typeof(RequestBuilderTestData))]
        internal void TestTheory(ExchangeRatesConfiguration configuration, string expected)
        {
            Assert.Equal(expected, new RequestBuilder(configuration).Build());
        }
    }

    public class RequestBuilderTestData : IEnumerable<object[]>
    {
        private readonly Currency @base = Currency.USD;
        private readonly Currency[] symbols = new[] { Currency.CHF, Currency.GBP };
        private readonly DateTime date = new DateTime(2000, 12, 31);
        private readonly DateTime startAt = new DateTime(2000, 01, 01);
        private readonly DateTime endAt = new DateTime(2000, 12, 31);

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ExchangeRatesConfiguration(),
                "latest"
            };

            yield return new object[]
            {
                new ExchangeRatesConfiguration(date),
                "2000-12-31"
            };

            yield return new object[]
            {
                new ExchangeRatesConfiguration(startAt, endAt),
                "history?start_at=2000-01-01&end_at=2000-12-31"
            };

            yield return new object[]
            {
                new ExchangeRatesConfiguration
                {
                    Base = @base,
                    Symbols = symbols
                },
                "latest?base=USD&symbols=CHF,GBP"
            };

            yield return new object[]
            {
                new ExchangeRatesConfiguration(date)
                {
                    Base = @base,
                    Symbols = symbols
                },
                "2000-12-31?base=USD&symbols=CHF,GBP"
            };

            yield return new object[]
            {
                new ExchangeRatesConfiguration(startAt, endAt)
                {
                    Base = @base,
                    Symbols = symbols
                },
                "history?start_at=2000-01-01&end_at=2000-12-31&base=USD&symbols=CHF,GBP"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
