﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ExchangeRatesApi
{
    public sealed class ExchangeRates
    {
        public ILogger Logger { get; set; }

        private readonly string _apiAccessKey;

        private readonly HttpClient _client = new HttpClient();
        private readonly Uri _uri;

        public ExchangeRates(string apiAccessKey, bool useHttps = true)
        {
            _apiAccessKey = apiAccessKey;
            _uri = new($"{(useHttps ? "https" : "http")}://api.exchangeratesapi.io/v1/");
        }

        /// <summary>
        /// Get the latest foreign exchange reference rates.
        /// Rates are quoted against the Euro.
        /// </summary>
        /// <returns>The latest foreign exchange reference rates.</returns>
        public async Task<ExchangeRatesResponse> GetLastestExchangeRatesAsync()
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration()
            );

        /// <summary>
        /// Get the latest foreign exchange reference rates.
        /// </summary>
        /// <param name="base"></param>
        /// <returns>The latest foreign exchange reference rates.</returns>
        public async Task<ExchangeRatesResponse> GetLastestExchangeRatesAsync(Currency @base)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration
                {
                    Base = @base
                }
            );

        /// <summary>
        /// Get the latest foreign exchange reference rates.
        /// Rates are quoted against the Euro.
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns>The latest foreign exchange reference rates.</returns>
        public async Task<ExchangeRatesResponse> GetLastestExchangeRatesAsync(IEnumerable<Currency> symbols)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration
                {
                    Symbols = symbols
                }
            );

        /// <summary>
        /// Get the latest foreign exchange reference rates.
        /// </summary>
        /// <param name="base"></param>
        /// <param name="symbols"></param>
        /// <returns>The latest foreign exchange reference rates.</returns>
        public async Task<ExchangeRatesResponse> GetLastestExchangeRatesAsync(Currency @base, IEnumerable<Currency> symbols)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration
                {
                    Base = @base,
                    Symbols = symbols
                }
            );

        /// <summary>
        /// Get historical rates for any day since 1999.
        /// Rates are quoted against the Euro.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>The historical rates</returns>
        public async Task<ExchangeRatesResponse> GetExchangeRatesByDateAsync(DateTime date)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration(date)
            );

        /// <summary>
        /// Get historical rates for any day since 1999.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="base"></param>
        /// <returns>The historical rates</returns>
        public async Task<ExchangeRatesResponse> GetExchangeRatesByDateAsync(DateTime date, Currency @base)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration(date)
                {
                    Base = @base
                }
            );

        /// <summary>
        /// Get historical rates for any day since 1999.
        /// Rates are quoted against the Euro.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="symbols"></param>
        /// <returns>The historical rates</returns>
        public async Task<ExchangeRatesResponse> GetExchangeRatesByDateAsync(DateTime date, IEnumerable<Currency> symbols)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration(date)
                {
                    Symbols = symbols
                }
            );

        /// <summary>
        /// Get historical rates for any day since 1999.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="base"></param>
        /// <param name="symbols"></param>
        /// <returns>The historical rates</returns>
        public async Task<ExchangeRatesResponse> GetExchangeRatesByDateAsync(DateTime date, Currency @base, IEnumerable<Currency> symbols)
            => await this.GetExchangeRatesAsync<ExchangeRatesResponse>(
                new ExchangeRatesConfiguration(date)
                {
                    Base = @base,
                    Symbols = symbols
                }
            );

        /// <summary>
        /// Get historical rates for a time period.
        /// Rates are quoted against the Euro.
        /// </summary>
        /// <param name="startAt"></param>
        /// <param name="endAt"></param>
        /// <returns>The historical rates for a time period.</returns>
        public async Task<ExchangeRatesHistoricalResponse> GetHistoricalExchangeRatesAsync(DateTime startAt, DateTime endAt)
            => await this.GetExchangeRatesAsync<ExchangeRatesHistoricalResponse>(
                new ExchangeRatesConfiguration(startAt, endAt)
            );

        /// <summary>
        /// Get historical rates for a time period.
        /// </summary>
        /// <param name="startAt"></param>
        /// <param name="endAt"></param>
        /// <param name="base"></param>
        /// <returns>The historical rates for a time period.</returns>
        public async Task<ExchangeRatesHistoricalResponse> GetHistoricalExchangeRatesAsync(DateTime startAt, DateTime endAt, Currency @base)
            => await this.GetExchangeRatesAsync<ExchangeRatesHistoricalResponse>(
                new ExchangeRatesConfiguration(startAt, endAt)
                {
                    Base = @base
                }
            );

        /// <summary>
        /// Get historical rates for a time period.
        /// Rates are quoted against the Euro.
        /// </summary>
        /// <param name="startAt"></param>
        /// <param name="endAt"></param>
        /// <param name="symbols"></param>
        /// <returns>The historical rates for a time period.</returns>
        public async Task<ExchangeRatesHistoricalResponse> GetHistoricalExchangeRatesAsync(DateTime startAt, DateTime endAt, IEnumerable<Currency> symbols)
            => await this.GetExchangeRatesAsync<ExchangeRatesHistoricalResponse>(
                new ExchangeRatesConfiguration(startAt, endAt)
                {
                    Symbols = symbols
                }
            );

        /// <summary>
        /// Get historical rates for a time period.
        /// </summary>
        /// <param name="startAt"></param>
        /// <param name="endAt"></param>
        /// <param name="base"></param>
        /// <param name="symbols"></param>
        /// <returns>The historical rates for a time period.</returns>
        public async Task<ExchangeRatesHistoricalResponse> GetHistoricalExchangeRatesAsync(DateTime startAt, DateTime endAt, Currency @base, IEnumerable<Currency> symbols)
            => await this.GetExchangeRatesAsync<ExchangeRatesHistoricalResponse>(
                new ExchangeRatesConfiguration(startAt, endAt)
                {
                    Base = @base,
                    Symbols = symbols
                }
            );

        private async Task<T> GetExchangeRatesAsync<T>(ExchangeRatesConfiguration configuration)
        {
            var request = this.GetRequestString(configuration);
            this.Log(request);

            var response = await this.GetResponse(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return this.Deserialize<T>(content);
        }

        private string GetRequestString(ExchangeRatesConfiguration configuration)
            => new RequestBuilder(configuration).Build(_apiAccessKey);

        private async Task<HttpResponseMessage> GetResponse(string request)
            => await _client.GetAsync($"{_uri}{request}");

        private T Deserialize<T>(string json)
            => JsonSerializer.Deserialize<T>(json);

        private void Log(string message)
        {
            if (this.Logger != null)
                this.Logger.LogDebug(message);
        }
    }
}
