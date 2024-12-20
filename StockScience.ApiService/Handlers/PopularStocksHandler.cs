﻿using StockScience.PriceApi.Domain.Stocks;
using StockScience.PriceApi.Models;
using StockScience.PriceApi.Repositories;

namespace StockScience.PriceApi.Handlers
{
    public class PopularStocksHandler(StocksRepository stocksRepository, PriceRequestsRepository priceRequestsRepository, StockPopularity stockPopularity)
    {
        public IReadOnlyList<PriceRequests> GetPopularStocks()
        {
            var stocks = stocksRepository.GetStocks();

            var stockRequests = 
                stocks
                .Select(stock => priceRequestsRepository.GetPriceRequests(stock.Symbol))
                .ToList();
            
            return stockPopularity.CalculatePopularStocks(stockRequests);
        }
    }
}
