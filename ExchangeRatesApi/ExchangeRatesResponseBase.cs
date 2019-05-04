using Newtonsoft.Json;

namespace ExchangeRatesApi
{
    public abstract class ExchangeRatesResponseBase
    {
        [JsonProperty("base")]
        public string Base { get; internal set; }
    }
}
