using StockScience.PriceApi.Models;
using StockScience.PriceApi.Repositories;

namespace StockScience.PriceApi.Handlers
{
    public class PopularStocksHandler(StocksRepository stocksRepository, PriceRequestsRepository priceRequestsRepository)
    {
        public IReadOnlyList<PriceRequests> GetPopularStocks()
        {
            var stocks = stocksRepository.GetStocks();

            var stockPopularity = stocks.Select(stock => priceRequestsRepository.GetPriceRequests(stock.Symbol))
                                        .OrderByDescending(requests => requests.RequestCount)
                                        .Take(3)
                                        .ToList();

            return stockPopularity;
        }
    }
}
