using StockScience.PriceApi.Domain.Prices;
using StockScience.PriceApi.Models;
using StockScience.PriceApi.Repositories;
using System.Threading.Tasks.Dataflow;

namespace StockScience.PriceApi.PriceProduction
{
    public class PricesProducer(PricesRepository pricesRepository, StockPriceCalculator stockPriceCalculator)
    {
        public async Task ProducePrice(string symbol, BufferBlock<StockPrice> pricesBuffer)
        {
            Console.WriteLine($"Producing price for {symbol}");
            var lastprice = pricesRepository.GetLastPrice(symbol);
            var price = await stockPriceCalculator.CalculatePrice(symbol, lastprice?.Price);
            Console.WriteLine($"Produced price ${price}");
            pricesBuffer.Post(price);
        }
    }
}
