using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ExchangeRatesApi
{
    public class ExchangeRatesResponse : ExchangeRatesResponseBase
    {
        [JsonProperty("date")]
        public DateTime Date { get; internal set; }
        [JsonProperty("rates")]
        public IDictionary<string, decimal> Rates { get; internal set; } = new Dictionary<string, decimal>();
    }
}
