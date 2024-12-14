using StockScience.PriceApi.Domain.Prices;
using StockScience.PriceApi.Repositories;
using System.Globalization;

namespace StockScience.PriceApi.Handlers
{
    public class StockHandler(IPricesRepository pricesRepository)
    {
        public int? GetPrice(string symbol)
        {
            Console.WriteLine($"Getting price for {symbol}");
            var lastPrice = pricesRepository.GetLastPrice(symbol);
            if (lastPrice == null)
            {
                Console.WriteLine($"Price not found for {symbol}");
                return null;
            }

            Console.WriteLine($"Got price for {symbol}: ${lastPrice.Price} at ${lastPrice.DateTime.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture)}");
            return lastPrice.Price;
        }
    }
}
