using StockScience.PriceApi.Models;

namespace StockScience.PriceApi.Repositories
{
    public class PricesRepository
    {
        private Dictionary<string, Stack<StockPrice>> prices = new Dictionary<string, Stack<StockPrice>>();

        public StockPrice? GetLastPrice(string symbol)
        {
            if(prices.ContainsKey(symbol))
            {
                return prices[symbol].Peek();
            }

            return null;
        }

        public void AddPrice(StockPrice price)
        {
            if (!prices.ContainsKey(price.Symbol))
            {
                prices[price.Symbol] = new Stack<StockPrice>();
            }

            prices[price.Symbol].Push(price);
        }
    }
}
