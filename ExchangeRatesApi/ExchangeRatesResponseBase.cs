using System.Text.Json.Serialization;

namespace ExchangeRatesApi
{
    public abstract class ExchangeRatesResponseBase
    {
        [JsonPropertyName("base")]
        public string Base { get; internal set; }
    }
}
