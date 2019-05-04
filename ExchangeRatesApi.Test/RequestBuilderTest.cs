using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace ExchangeRatesApi.Test
{
    public class RequestBuilderTest
    {
        private readonly RequestBuilder sut = new RequestBuilder();

        [Theory]
        [ClassData(typeof(RequestBuilderTestData))]
        public void Test1(ExchangeRatesConfiguration configuration, string expected)
        {
            Assert.Equal(expected, this.sut.Build(configuration));
        }
    }

    public class RequestBuilderTestData : IEnumerable<object[]>
    {
        private readonly string @base = "USD";
        private readonly string[] symbols = new[] { "CHF", "GBP" };
        private readonly DateTime date = new DateTime(2000,12,31);
        private readonly DateTime startAt = new DateTime(2000,01,01);
        private readonly DateTime endAt = new DateTime(2000, 12, 31);

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] 
                { new ExchangeRatesConfiguration(), "latest?base=EUR" };
            yield return new object[] 
                { new ExchangeRatesDateConfiguration(date), "2000-12-31?base=EUR" };
            yield return new object[]
                { new ExchangeRatesHistoricalConfiguration(startAt, endAt), "history?base=EUR&start_at=2000-01-01&end_at=2000-12-31"};
            yield return new object[]
                { new ExchangeRatesConfiguration(@base, symbols), "latest?base=USD&symbols=CHF,GBP" };
            yield return new object[]
                { new ExchangeRatesDateConfiguration(@base, symbols, date), "2000-12-31?base=USD&symbols=CHF,GBP" };
            yield return new object[]

                { new ExchangeRatesHistoricalConfiguration(@base, symbols, startAt, endAt), "history?base=USD&start_at=2000-01-01&end_at=2000-12-31&symbols=CHF,GBP"};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
