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
    }
}
