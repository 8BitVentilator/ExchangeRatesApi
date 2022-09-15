using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExchangeRatesApi
{
    public class ExchangeRatesHistoricalResponse : ExchangeRatesResponseBase
    {
        [JsonPropertyName("rates")]
        public IDictionary<DateTime, IDictionary<string, decimal>> Rates { get; internal set; } = new Dictionary<DateTime, IDictionary<string, decimal>>();
    }
}
