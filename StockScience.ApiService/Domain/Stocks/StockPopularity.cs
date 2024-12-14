using StockScience.PriceApi.Models;

namespace StockScience.PriceApi.Domain.Stocks
{
    public class StockPopularity
    {
        public IReadOnlyList<PriceRequests> CalculatePopularStocks(IReadOnlyCollection<PriceRequests> priceRequests)
        {
            var stockPopularity =
                priceRequests
                .OrderByDescending(requests => requests.RequestCount)
                .Take(3)
                .ToList();

            return stockPopularity;
        }
    }
}
