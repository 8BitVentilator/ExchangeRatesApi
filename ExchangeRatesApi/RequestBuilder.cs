using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ExchangeRatesApi.Test")]
namespace ExchangeRatesApi
{
    internal class RequestBuilder
    {
        private const string DATE_FORMAT = "yyyy-MM-dd";

        private ExchangeRatesConfiguration configuration;

        public string Build(ExchangeRatesConfiguration configuration)
        {
            this.configuration = configuration;

            return $"{this.GetEndpint()}?{string.Join("&", this.GetParameters())}";
        }

        private string GetEndpint()
            => configuration.HasDate
                ? configuration.Date.ToString(DATE_FORMAT)
                : configuration.HasHistoricalDates
                    ? "history"
                    : "latest";

        private IEnumerable<string> GetParameters()
        {
            yield return this.GetBase();

            if (configuration.HasHistoricalDates)
                yield return this.GetStartAt();
            if (configuration.HasHistoricalDates)
                yield return this.GetEndAt();
            if (this.HasSymbols())
                yield return this.GetSymbols();
        }

        private bool HasSymbols() 
            => configuration.Symbols.Where(x => !string.IsNullOrWhiteSpace(x)).Any();

        private string GetStartAt()
            => $"start_at={configuration.StartAt.ToString(DATE_FORMAT)}";

        private string GetEndAt()
            => $"end_at={configuration.EndAt.ToString(DATE_FORMAT)}";

        private string GetBase()
            => $"base={configuration.Base}";

        private string GetSymbols()
            => $"symbols={string.Join(",", configuration.Symbols.Where(x => !string.IsNullOrWhiteSpace(x)))}";
    }
}
