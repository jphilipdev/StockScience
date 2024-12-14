namespace StockScience.PriceApi.Domain.Prices
{
    public class StockPriceCalculator
    {
        private Random random = new Random();

        public async Task<int> CalculatePrice(string symbol, int lastPrice)
        {
            await Task.Delay(800);
            return Math.Max(0, random.Next(lastPrice - 10, lastPrice + 10));
        }
    }
}
