using StockScience.PriceApi.Repositories;
using System.ComponentModel;

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
                var priceProductionManager = scope.ServiceProvider.GetRequiredService<PriceProductionManager>();


                var stocks = stocksRepository.GetStocks();

                foreach (var stock in stocks)
                {
                    BackgroundWorker worker = new BackgroundWorker();

                    worker.DoWork += (sender, e) =>
                    {
                        priceProductionManager.ProducePrices(stock.Symbol, cancellationToken);
                    };

                    worker.RunWorkerAsync();
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"{nameof(PriceProducerService)}: Stopping");

            await base.StopAsync(cancellationToken);
        }
    }
}