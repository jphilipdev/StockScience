using StockScience.PriceApi.Models;
using System.Collections.Concurrent;

namespace StockScience.PriceApi.Repositories
{
    public class PricesRepository
    {
        private Dictionary<string, ConcurrentStack<StockPrice>> prices = new Dictionary<string, ConcurrentStack<StockPrice>>();

        public StockPrice? GetLastPrice(string symbol)
        {
            if(prices.ContainsKey(symbol) && prices[symbol].TryPeek(out var price))
            {
                return price;
            }

            return null;
        }

        public StockPrice? GetPrice(string symbol, DateTime atTime)
        {
            var lastPrice = GetLastPrice(symbol);
            if (lastPrice == null)
            {
                return null;
            }

            if (lastPrice.DateTime < atTime)
            {
                return null;
            }

            return prices[symbol].SingleOrDefault(price => price.DateTime == atTime);
        }

        public void AddPrice(StockPrice price)
        {
            if (!prices.ContainsKey(price.Symbol))
            {
                prices[price.Symbol] = new ConcurrentStack<StockPrice>();
            }

            prices[price.Symbol].Push(price);
        }
    }
}
