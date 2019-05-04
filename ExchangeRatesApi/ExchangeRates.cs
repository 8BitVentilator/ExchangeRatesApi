using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace ExchangeRatesApi
{
    public class ExchangeRates
    {
        public ILogger Logger { get; set; }

        private readonly HttpClient client = new HttpClient();
        private readonly Uri uri = new Uri("https://api.exchangeratesapi.io");

        public async Task<ExchangeRatesResponse> GetLastestExchangeRatesAsync()
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration()
            );

        public async Task<ExchangeRatesResponse> GetLastestExchangeRatesAsync(Currency @base)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration
                {
                    Base = @base
                }
            );

        public async Task<ExchangeRatesResponse> GetLastestExchangeRatesAsync(IEnumerable<Currency> symbols)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration
                {
                    Symbols = symbols ?? throw new ArgumentNullException(nameof(symbols))
                }
            );

        public async Task<ExchangeRatesResponse> GetLastestExchangeRatesAsync(Currency @base, IEnumerable<Currency> symbols)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration
                {
                    Base = @base,
                    Symbols = symbols ?? throw new ArgumentNullException(nameof(symbols))
                }
            );

        public async Task<ExchangeRatesResponse> GetExchangeRatesByDateAsync(DateTime date)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration(date)
            );

        public async Task<ExchangeRatesResponse> GetExchangeRatesByDateAsync(DateTime date, Currency @base)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration(date)
                {
                    Base = @base
                }
            );

        public async Task<ExchangeRatesResponse> GetExchangeRatesByDateAsync(DateTime date, IEnumerable<Currency> symbols)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration(date)
                {
                    Symbols = symbols ?? throw new ArgumentNullException(nameof(symbols))
                }
            );

        public async Task<ExchangeRatesResponse> GetExchangeRatesByDateAsync(DateTime date, Currency @base, IEnumerable<Currency> symbols)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration(date)
                {
                    Base = @base,
                    Symbols = symbols ?? throw new ArgumentNullException(nameof(symbols))
                }
            );

        public async Task<ExchangeRatesHistoricalResponse> GetHistoricalExchangeRatesAsync(DateTime startAt, DateTime endAt)
            => await this.GetExchangeRatesAsync<ExchangeRatesHistoricalResponse>(
                new ExchangeRatesConfiguration(startAt, endAt)
            );

        public async Task<ExchangeRatesHistoricalResponse> GetHistoricalExchangeRatesAsync(DateTime startAt, DateTime endAt, Currency @base)
            => await this.GetExchangeRatesAsync<ExchangeRatesHistoricalResponse>(
                new ExchangeRatesConfiguration(startAt, endAt)
                {
                    Base = @base
                }
            );

        public async Task<ExchangeRatesHistoricalResponse> GetHistoricalExchangeRatesAsync(DateTime startAt, DateTime endAt, IEnumerable<Currency> symbols)
            => await this.GetExchangeRatesAsync<ExchangeRatesHistoricalResponse>(
                new ExchangeRatesConfiguration(startAt, endAt)
                {
                    Symbols = symbols ?? throw new ArgumentNullException(nameof(symbols))
                }
            );

        public async Task<ExchangeRatesHistoricalResponse> GetHistoricalExchangeRatesAsync(DateTime startAt, DateTime endAt, Currency @base, IEnumerable<Currency> symbols)
            => await this.GetExchangeRatesAsync<ExchangeRatesHistoricalResponse>(
                new ExchangeRatesConfiguration(startAt, endAt)
                {
                    Base = @base,
                    Symbols = symbols ?? throw new ArgumentNullException(nameof(symbols))
                }
            );

        private async Task<T> GetExchangeRatesAsync<T>(ExchangeRatesConfiguration configuration)
        {
            var request = this.GetRequestString(configuration);
            this.Log(request);

            var response = await this.GetResponse(request);
            response.EnsureSuccessStatusCode();

            return this.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        private string GetRequestString(ExchangeRatesConfiguration configuration)
            => new RequestBuilder(configuration).Build();

        private async Task<HttpResponseMessage> GetResponse(string request)
            => await client.GetAsync($"{this.uri}{request}");

        private T Deserialize<T>(string json)
            => JsonConvert.DeserializeObject<T>(json);

        private void Log(string message)
        {
            if (this.Logger != null)
                this.Logger.LogDebug(message);
        }
    }
}
