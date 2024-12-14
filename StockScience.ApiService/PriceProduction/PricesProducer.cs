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

            var lastPrice = pricesRepository.GetLastPrice(symbol);
            if (lastPrice == null)
            {
                Console.WriteLine($"Could not produce price as no last price exists for {symbol}");
            }
            else
            {
                var price = await stockPriceCalculator.CalculatePrice(symbol, lastPrice.Price, lastPrice.DateTime.AddSeconds(1));

                Console.WriteLine($"Produced price ${price}");

                pricesBuffer.Post(price);
            }
        }
    }
}
