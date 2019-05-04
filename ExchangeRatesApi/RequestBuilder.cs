using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ExchangeRatesApi.Test")]
namespace ExchangeRatesApi
{
    internal class RequestBuilder
    {
        private const string DATE_FORMAT = "yyyy-MM-dd";

        private readonly ExchangeRatesConfiguration configuration;

        public RequestBuilder(ExchangeRatesConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Build()
        {
            var parameters = string.Join("&", this.GetParameters());

            return !string.IsNullOrWhiteSpace(parameters)
                    ? $"{this.GetEndpint()}?{parameters}"
                    : this.GetEndpint();
        }

        private string GetEndpint()
            => configuration.HasDate
                ? configuration.Date.ToString(DATE_FORMAT)
                : configuration.HasHistoricalDates
                    ? "history"
                    : "latest";

        private IEnumerable<string> GetParameters()
        {
            if (configuration.HasHistoricalDates)
                yield return this.GetStartAt();
            if (configuration.HasHistoricalDates)
                yield return this.GetEndAt();
            if (configuration.Base != ExchangeRatesConfiguration.DEFAULT_BASE)
                yield return this.GetBase();
            if (this.HasSymbols())
                yield return this.GetSymbols();
        }

        private bool HasSymbols()
            => configuration.Symbols.Any();

        private string GetStartAt()
            => $"start_at={configuration.StartAt.ToString(DATE_FORMAT)}";

        private string GetEndAt()
            => $"end_at={configuration.EndAt.ToString(DATE_FORMAT)}";

        private string GetBase()
            => $"base={configuration.Base}";

        private string GetSymbols()
            => $"symbols={string.Join(",", configuration.Symbols)}";
    }
}
