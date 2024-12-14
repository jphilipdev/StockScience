var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.StockScience_PriceApi>("PriceApi");

builder.Build().Run();
