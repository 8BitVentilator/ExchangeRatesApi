using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ExchangeRatesApi.Test")]
namespace ExchangeRatesApi
{
    internal class ExchangeRatesDateConfiguration : ExchangeRatesConfiguration
    {
        internal override bool HasDate => true;

        public ExchangeRatesDateConfiguration(DateTime date)
            : this(DEFAULT_BASE, date)
        { }

        public ExchangeRatesDateConfiguration(string @base, DateTime date)
            : this(@base, new string[0], date)
        { }

         public ExchangeRatesDateConfiguration(
            string @base,
            IEnumerable<string> symbols,
            DateTime date) : base(@base, symbols)
        {
            this.Date = date;
        }
    }
}
