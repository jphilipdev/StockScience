using StockScience.PriceApi.Models;
using System.Collections.Concurrent;

namespace StockScience.PriceApi.Repositories
{
    public class PriceRequestsRepository
    {
        private ConcurrentDictionary<string, int> priceRequests = new ConcurrentDictionary<string, int>();

        public void AddPriceRequest(string symbol)
        {
            var initialValue = 1;
            priceRequests.AddOrUpdate(symbol, initialValue, (_, lastRequestCount) => lastRequestCount + 1);
        }

        public PriceRequests GetPriceRequests(string symbol)
        {
            if(priceRequests.TryGetValue(symbol, out var symbolPriceRequests))
            {
                return new PriceRequests(symbol, symbolPriceRequests);
            }

            return new PriceRequests(symbol, 0);
        }
    }
}
