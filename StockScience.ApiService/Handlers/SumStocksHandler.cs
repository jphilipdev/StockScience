using StockScience.PriceApi.Repositories;
using StockScience.PriceApi.Utils;

namespace StockScience.PriceApi.Handlers
{
    public class SumStocksHandler(StocksRepository stocksRepository, PricesRepository pricesRepository)
    {
        public int GetSumOfStocks()
        {
            var now = DateTime.UtcNow.Trim(TimeSpan.TicksPerSecond);
            var stocks = stocksRepository.GetStocks();

            var currentStockPrices = new Dictionary<string, int>();

            while(currentStockPrices.Count < stocks.Count)
            {
                foreach(var stock in stocks)
                {
                    if(!currentStockPrices.ContainsKey(stock.Symbol))
                    {
                        var price = pricesRepository.GetPrice(stock.Symbol, now);
                        if(price == null)
                        {
                            Console.WriteLine($"No price yet for symbol: {stock.Symbol} at {now}");
                        }
                        else
                        {
                            currentStockPrices[stock.Symbol] = price.Price;
                        }
                    }
                }
            }

            return currentStockPrices.Values.Sum();
        }
    }
}
