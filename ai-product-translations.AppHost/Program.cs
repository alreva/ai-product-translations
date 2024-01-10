using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

builder.Services.AddDataProtection();

var cache = builder
    .AddRedis("cache");

var apiService = builder
    .AddProject<Projects.ApiService>("apiservice")
    .WithReference(cache);

builder.AddProject<Projects.Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
