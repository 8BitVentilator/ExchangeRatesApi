using System;
using System.Collections.Generic;

namespace ExchangeRatesApi
{
    public class ExchangeRatesConfiguration
    {
        protected const string DEFAULT_BASE = "EUR";

        public string Base { get; set; }
        public List<string> Symbols { get; internal set; }

        internal virtual bool HasDate { get; }
        internal DateTime Date { get; set; }

        internal virtual bool HasHistoricalDates { get; }
        internal DateTime StartAt { get; set; }
        internal DateTime EndAt { get; set; }

        public ExchangeRatesConfiguration()
            : this(DEFAULT_BASE)
        { }

        public ExchangeRatesConfiguration(string @base)
            : this(@base, new string[0])
        { }

        public ExchangeRatesConfiguration(string @base, IEnumerable<string> symbols)
        {
            this.Base = @base;
            this.Symbols = new List<string>(symbols);
        }
    }
}
