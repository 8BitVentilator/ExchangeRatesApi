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
        internal void TestTheory(ExchangeRatesConfiguration configuration, string apiAccessKey, string expected)
        {
            Assert.Equal(expected, new RequestBuilder(configuration).Build(apiAccessKey));
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
                "1234567890",
                "latest?access_key=1234567890"
            };

            yield return new object[]
            {
                new ExchangeRatesConfiguration(date),
                "1234567890",
                "2000-12-31?access_key=1234567890"
            };

            yield return new object[]
            {
                new ExchangeRatesConfiguration(startAt, endAt),
                "1234567890",
                "history?start_at=2000-01-01&end_at=2000-12-31&access_key=1234567890"
            };

            yield return new object[]
            {
                new ExchangeRatesConfiguration
                {
                    Base = @base,
                    Symbols = symbols
                },
                "1234567890",
                "latest?base=USD&symbols=CHF,GBP&access_key=1234567890"
            };

            yield return new object[]
            {
                new ExchangeRatesConfiguration(date)
                {
                    Base = @base,
                    Symbols = symbols
                },
                "1234567890",
                "2000-12-31?base=USD&symbols=CHF,GBP&access_key=1234567890"
            };

            yield return new object[]
            {
                new ExchangeRatesConfiguration(startAt, endAt)
                {
                    Base = @base,
                    Symbols = symbols
                },
                "1234567890",
                "history?start_at=2000-01-01&end_at=2000-12-31&base=USD&symbols=CHF,GBP&access_key=1234567890"
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
