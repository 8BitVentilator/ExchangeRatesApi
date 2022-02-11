using System;
using System.Collections.Generic;

namespace ExchangeRatesApi
{
    internal class ExchangeRatesConfiguration
    {
        public static readonly Currency DefaultBase = Currency.EUR;
        public static readonly DateTime MinDate = new(1999, 01, 04);

        public Currency Base { get; set; } = DefaultBase;

        private IEnumerable<Currency> symbols = Array.Empty<Currency>();
        public IEnumerable<Currency> Symbols
        {
            get => this.symbols;
            set => this.symbols = value ?? throw new ArgumentNullException(nameof(value));
        }

        public bool HasDate { get; } = false;
        public DateTime Date { get; }

        public bool HasHistoricalDates { get; } = false;
        public DateTime StartAt { get; }
        public DateTime EndAt { get; }

        public ExchangeRatesConfiguration()
        { }

        public ExchangeRatesConfiguration(DateTime date)
        {
            if (date < MinDate)
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
