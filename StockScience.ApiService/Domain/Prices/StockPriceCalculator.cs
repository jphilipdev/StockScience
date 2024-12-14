using StockScience.PriceApi.Models;

namespace StockScience.PriceApi.Domain.Prices
{
    public class StockPriceCalculator
    {
        private Random random = new Random();

        public async Task<StockPrice> CalculatePrice(string symbol, int? lastPrice)
        {
            await Task.Delay(800);

            lastPrice = lastPrice ?? 1000;
            var price = Math.Max(0, random.Next(lastPrice.Value - 10, lastPrice.Value + 10));

            return new StockPrice(symbol, price, DateTime.UtcNow);
        }
    }
}
