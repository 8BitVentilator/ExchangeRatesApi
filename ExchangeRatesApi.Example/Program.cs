using System;
using System.Linq;

namespace ExchangeRatesApi.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var api = new ExchangeRates();
            var response = api.GetExchangeRatesByDateAsync(new DateTime(1999, 01, 01)).Result;
            Console.WriteLine($"Base: {response.Base}");
            Console.WriteLine($"Date: {response.Date}");
            Console.WriteLine("Rates:");
            Console.WriteLine(string.Join(Environment.NewLine, response.Rates.Select(x => $"Currency: {x.Key} Rate: {x.Value}")));
            Console.ReadKey(); 
        }
    }
}