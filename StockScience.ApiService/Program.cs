using StockScience.PriceApi.Handlers;
using StockScience.PriceApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddSingleton<StockHandler>();
builder.Services.AddSingleton<IPricesRepository, PricesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();


app.MapGet("/stock/{symbol}", (StockHandler stockHandler, string symbol) =>
{
    return stockHandler.GetPrice(symbol);
})
.WithName("GetStockPrice");

app.MapDefaultEndpoints();

app.Run();
