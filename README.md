# ExchangeRatesApi
An unofficial C# wrapper for the ExchangeRatesAPI. 

This is a unofficial C# wrapper for the free ExchangeRatesAPI, which is a free service for current and historical foreign exchange rates 
published by the European Central Bank.

## Usage
First, create an instance of the wrapper.
```csharp
var api = new ExchangeRates();
```
Get the latest foreign exchange reference rates.
```csharp
var response = await api.GetLastestExchangeRatesAsync();
```
Get historical rates for any day since 1999.
```csharp
var response = await api.GetExchangeRatesByDateAsync(new DateTime(2010, 01, 12));
```
Rates are quoted against the Euro by default. Quote against a different currency by setting the base parameter in your request.
```csharp
var response = await api.GetLastestExchangeRatesAsync(Currency.USD);
```
Request specific exchange rates by setting the symbols parameter.
```csharp
var response = await api.GetLastestExchangeRatesAsync(new [] {Currency.USD, Currency.GBP });
```
Get historical rates for a time period.
```csharp
var response = await api.GetHistoricalExchangeRatesAsync(new DateTime(2018, 01, 01), new DateTime(2018, 09, 01));
```
Limit results to specific exchange rates to save bandwidth with the symbols parameter.
```csharp
var response = await api.GetHistoricalExchangeRatesAsync(new DateTime(2018, 01, 01), new DateTime(2018, 09, 01), new [] { Currency.ILS, Currency.JPY });
```
Quote the historical rates against a different currency.
```csharp
var response = await api.GetHistoricalExchangeRatesAsync(new DateTime(2018, 01, 01), new DateTime(2018, 09, 01), Currency.USD);
```
