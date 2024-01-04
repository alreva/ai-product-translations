using AiProductTranslations.ApiService.Catalog;
using AiProductTranslations.ApiService.TranslationService;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
    
// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisDistributedCache("cache");

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<IProductDescriptionService, OpenAiProductDescriptionService>();
builder.Services.Decorate<IProductDescriptionService, RedisCacheDelegatingProductDescriptionService>();
builder.Services.AddSingleton<ICatalogService, RedisCacheCatalogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapGet("/product-descriptions/sample", async (IProductDescriptionService svc) =>
{
    var product = new Product(
        "Men's Running Shoes",
        ["Lightweight", "breathable fabric", "cushioned insole", "durable rubber outsole"],
        "Blue",
        "7-12",
        "$80");

    return await svc.GenerateProductDescription(product);
});
app.MapPost("/product-descriptions/gen",
    async ([FromBody]Product product, IProductDescriptionService svc) => await svc.GenerateProductDescription(product));

app.MapGet("/catalog/sample", async (ICatalogService svc) => await svc.SampleCatalog());
app.MapGet("/catalog", async (ICatalogService svc) => await svc.Get());
app.MapPost("/catalog", async (
    [FromBody]CatalogProduct[] products, ICatalogService svc) => await svc.Upload(products));

app.MapDefaultEndpoints();

app.Run();