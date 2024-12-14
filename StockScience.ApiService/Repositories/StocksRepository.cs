using StockScience.PriceApi.Models;

namespace StockScience.PriceApi.Repositories
{
    public class StocksRepository
    {
        public IReadOnlyCollection<Stock> GetStocks()
        {
            return new List<Stock>
            {
                new Stock("stock-1"),
                new Stock("stock-2"),
                new Stock("stock-3"),
                new Stock("stock-4"),
                new Stock("stock-5"),
                new Stock("stock-6"),
                new Stock("stock-7"),
                new Stock("stock-8"),
                new Stock("stock-9"),
                new Stock("stock-10"),
            };
        }
    }
}
