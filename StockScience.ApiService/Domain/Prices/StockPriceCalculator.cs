using StockScience.PriceApi.Models;

namespace StockScience.PriceApi.Domain.Prices
{
    public class StockPriceCalculator
    {
        private Random random = new Random();

        public async Task<StockPrice> CalculatePrice(string symbol, int lastPrice, DateTime dateTime)
        {
            await Task.Delay(1000);

            var price = Math.Max(0, random.Next(lastPrice - 10, lastPrice + 10));

            return new StockPrice(symbol, price, dateTime);
        }        
    }
}
