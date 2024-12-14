using StockScience.PriceApi.Repositories;

namespace StockScience.PriceApi.Handlers
{
    public class StockHandler(PricesRepository pricesRepository, PriceRequestsRepository priceRequestsRepository)
    {
        public int? GetPrice(string symbol)
        {
            Console.WriteLine($"Getting price for {symbol}");
            priceRequestsRepository.AddPriceRequest(symbol);

            var lastPrice = pricesRepository.GetLastPrice(symbol);
            if (lastPrice == null)
            {
                Console.WriteLine($"Price not found for {symbol}");
                return null;
            }

            Console.WriteLine($"Got price {lastPrice}");
            return lastPrice.Price;
        }
    }
}
