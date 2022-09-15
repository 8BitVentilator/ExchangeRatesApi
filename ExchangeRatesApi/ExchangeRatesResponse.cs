using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExchangeRatesApi
{
    public class ExchangeRatesResponse : ExchangeRatesResponseBase
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; internal set; }
        [JsonPropertyName("rates")]
        public IDictionary<string, decimal> Rates { get; internal set; } = new Dictionary<string, decimal>();
    }
}
