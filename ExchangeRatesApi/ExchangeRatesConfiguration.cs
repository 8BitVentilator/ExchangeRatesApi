using System;
using System.Collections.Generic;

namespace ExchangeRatesApi
{
    internal class ExchangeRatesConfiguration
    {
        public static readonly Currency DEFAULT_BASE = Currency.EUR;
        public static readonly DateTime MIN_DATE = new DateTime(1999, 01, 04);

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
            if (date < MIN_DATE)
                throw new ArgumentOutOfRangeException(
                    nameof(date),
                    ExchangeRatesConfigurationRes.NoDataExceptionMessage
                );

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
