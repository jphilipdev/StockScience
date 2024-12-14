using StockScience.PriceApi.Repositories;
using StockScience.PriceApi.Utils;
using StockScience.PriceApi.Models;
using System.ComponentModel;
using System.Threading;

namespace StockScience.PriceApi.PriceProduction
{
    public class PriceProducerService : BackgroundService
    {
        public PriceProducerService(IServiceProvider services)
        {
            Services = services;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(PriceProducerService)}: Starting");

            using (var scope = Services.CreateScope())
            {
                var stocksRepository = scope.ServiceProvider.GetRequiredService<StocksRepository>();
                var pricesRepository = scope.ServiceProvider.GetRequiredService<PricesRepository>();
                var priceProductionManager = scope.ServiceProvider.GetRequiredService<PriceProductionManager>();

                var initialPriceTime = DateTime.UtcNow.Trim(TimeSpan.TicksPerSecond);
                var stocks = stocksRepository.GetStocks();

                foreach (var stock in stocks)
                {
                    InitialiseStockPrice(pricesRepository, stock, initialPriceTime);
                    ProducePricesForStock(priceProductionManager, stock, cancellationToken);
                }
            }
        }

        private void InitialiseStockPrice(PricesRepository pricesRepository, Stock stock, DateTime initialPriceTime)
        {
            pricesRepository.AddPrice(new StockPrice(stock.Symbol, 1000, initialPriceTime));
        }

        private void ProducePricesForStock(PriceProductionManager priceProductionManager, Stock stock, CancellationToken cancellationToken)
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (sender, e) =>
            {
                priceProductionManager.ProducePrices(stock.Symbol, cancellationToken);
            };

            worker.RunWorkerAsync();
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(PriceProducerService)}: Stopping");

            await base.StopAsync(cancellationToken);
        }
    }
}