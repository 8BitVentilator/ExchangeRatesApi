using System;
using System.Collections.Generic;

namespace ExchangeRatesApi
{
    internal class ExchangeRatesConfiguration
    {
        public const Currency DEFAULT_BASE = Currency.EUR;

        public Currency Base { get; set; } = DEFAULT_BASE;
        public IEnumerable<Currency> Symbols { get; set; } = new Currency[0];

        public bool HasDate { get; } = false;
        public DateTime Date { get; }

        public bool HasHistoricalDates { get; } = false;
        public DateTime StartAt { get; }
        public DateTime EndAt { get; }

        public ExchangeRatesConfiguration()
        { }

        public ExchangeRatesConfiguration(DateTime date)
        {
            this.HasDate = true;
            this.Date = date;
        }

        public ExchangeRatesConfiguration(DateTime startAt, DateTime endAt)
        {
            this.HasHistoricalDates = true;
            this.StartAt = startAt;
            this.EndAt = endAt;
        }
    }
}
