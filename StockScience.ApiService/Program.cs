using StockScience.PriceApi.Domain.Prices;
using StockScience.PriceApi.Handlers;
using StockScience.PriceApi.PriceProduction;
using StockScience.PriceApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddHostedService<PriceProducerService>();
builder.Services.AddSingleton<PriceProductionManager>();
builder.Services.AddSingleton<PricesProducer>();

builder.Services.AddSingleton<StockHandler>();
builder.Services.AddSingleton<PopularStocksHandler>();

builder.Services.AddSingleton<PricesRepository>();
builder.Services.AddSingleton<StocksRepository>();
builder.Services.AddSingleton<PriceRequestsRepository>();

builder.Services.AddSingleton<StockPriceCalculator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();


app.MapGet("/stock/{symbol}", (StockHandler stockHandler, string symbol) =>
{
    return stockHandler.GetPrice(symbol);
})
.WithName("GetStockPrice");

app.MapGet("/popular-stocks", (PopularStocksHandler popularStocksHandler) =>
{
    return popularStocksHandler.GetPopularStocks();
})
.WithName("GetPopularStocks");

app.MapDefaultEndpoints();

app.Run();
