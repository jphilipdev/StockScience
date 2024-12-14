using StockScience.PriceApi.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();


app.MapGet("/stock/{symbol}", (string symbol) =>
{
    return new StockHandler().GetPrice(symbol);
})
.WithName("GetStockPrice");

app.MapDefaultEndpoints();

app.Run();
