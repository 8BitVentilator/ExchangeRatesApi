using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace ExchangeRatesApi
{
    public class ExchangeRates
    {
        public ILogger Logger { get; set; }
        public ExchangeRatesConfiguration Configuration { get; private set; }

        private readonly HttpClient client = new HttpClient();
        private readonly RequestBuilder builder = new RequestBuilder();
        private readonly Uri uri = new Uri("https://api.exchangeratesapi.io");

        public ExchangeRates()
            : this(new ExchangeRatesConfiguration())
        { }

        public ExchangeRates(ExchangeRatesConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public async Task<ExchangeRatesResponse> GetLastestExchangeRatesAsync()
        {
            this.Configuration = new ExchangeRatesConfiguration(
                this.Configuration.Base, 
                this.Configuration.Symbols
            );

            return await this.GetExchangeRatesAsync<ExchangeRatesResponse>();
        }

        public async Task<ExchangeRatesResponse> GetExchangeRatesByDateAsync(DateTime date)
        {
            this.Configuration = new ExchangeRatesDateConfiguration(
                this.Configuration.Base,
                this.Configuration.Symbols,
                date
            );

            return await this.GetExchangeRatesAsync<ExchangeRatesResponse>();
        }

        public async Task<ExchangeRatesHistoricalResponse> GetHistoricalExchangeRatesAsync(DateTime startAt, DateTime endAt)
        {

            this.Configuration = new ExchangeRatesHistoricalConfiguration(
                this.Configuration.Base,
                this.Configuration.Symbols,
                startAt,
                endAt
            );

            return await this.GetExchangeRatesAsync<ExchangeRatesHistoricalResponse>();
        }

        private async Task<T> GetExchangeRatesAsync<T>()
        {
            var parameters = this.builder.Build(this.Configuration);
            this.Log(parameters);
            
            var response = await client.GetAsync($"{this.uri}{parameters}");
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<T>(
                await response.Content.ReadAsStringAsync()
            );
        }

        private void Log(string message)
        {
            if (this.Logger != null)
                this.Logger.LogDebug(message);
        }
    }
}
