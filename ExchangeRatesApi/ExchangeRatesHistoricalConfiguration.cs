using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ExchangeRatesApi.Test")]
namespace ExchangeRatesApi
{
    internal class ExchangeRatesHistoricalConfiguration : ExchangeRatesConfiguration
    {
        internal override bool HasHistoricalDates => true;

        public ExchangeRatesHistoricalConfiguration(DateTime startAt, DateTime endAt)
            : this(DEFAULT_BASE, startAt, endAt)
        { }

        public ExchangeRatesHistoricalConfiguration(string @base, DateTime startAt, DateTime endAt)
            : this(@base, new string[0], startAt, endAt)
        { }

        public ExchangeRatesHistoricalConfiguration(
            string @base,
            IEnumerable<string> symbols,
            DateTime startAt,
            DateTime endAt) : base(@base, symbols)
        {
            this.StartAt = startAt;
            this.EndAt = endAt;
        }
    }
}
