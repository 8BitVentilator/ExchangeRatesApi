using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ExchangeRatesApi
{
    public class ExchangeRatesHistoricalResponse : ExchangeRatesResponseBase
    {
        [JsonProperty("rates")]
        public IDictionary<DateTime, IDictionary<string, decimal>> Rates { get; internal set; } = new Dictionary<DateTime, IDictionary<string, decimal>>();
    }
}
