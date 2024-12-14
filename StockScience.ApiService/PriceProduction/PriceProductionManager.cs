using StockScience.PriceApi.Models;
using StockScience.PriceApi.Repositories;
using System.Threading.Tasks.Dataflow;

namespace StockScience.PriceApi.PriceProduction
{
    public class PriceProductionManager(PricesRepository pricesRepository, PricesProducer pricesProducer)
    {
        private BufferBlock<StockPrice> pricesBuffer = new BufferBlock<StockPrice>();

        public async Task ProducePrices(string symbol, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {                    
                    pricesProducer.ProducePrice(symbol, pricesBuffer);

                    Console.WriteLine($"Waiting for price for {symbol}");
                    var price = await pricesBuffer.ReceiveAsync();

                    Console.WriteLine($"Received price: {price}");
                    pricesRepository.AddPrice(price);

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Error when producing price for {symbol}", e);
                }
            }
        }
    }
}
