using StockScience.PriceApi.Models;

namespace StockScience.PriceApi.Domain.Prices
{
    public class StockPriceCalculator
    {
        private Random random = new Random();

        public async Task<StockPrice> CalculatePrice(string symbol, int lastPrice)
        {
            await Task.Delay(800);
            var price = Math.Max(0, random.Next(lastPrice - 10, lastPrice + 10));
            return new StockPrice(symbol, price, DateTime.UtcNow);
        }
    }
}
