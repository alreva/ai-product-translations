using AiProductTranslations.ApiService.Ai;
using AiProductTranslations.ApiService.Ai.Descriptions;
using AiProductTranslations.ApiService.Ai.Translations;
using AiProductTranslations.ApiService.Catalog;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
    
// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisDistributedCache("cache");

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services
    .AddTransient<IProductDescriptionService, OpenAiProductDescriptionService>()
    .Decorate<IProductDescriptionService, RedisCachingProductDescriptionService>();
builder.Services
    .AddTransient<IProductTranslationService, OpenAiProductTranslationService>()
    .Decorate<IProductTranslationService, RedisCacheDelegatingProductTranslationService>();
builder.Services.AddSingleton<ICatalogService, RedisCacheCatalogService>();

var apiKey = builder.Configuration["OpenAI:ApiKey"]!;
builder.Services.AddHttpClient<OpenAiClient>(client =>
{
    client.BaseAddress = new Uri("https://api.openai.com/v1/chat/completions");
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
});

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

app.MapPost("/product-descriptions/gen/{lang=English}",
    async (
            [FromRoute] string lang,
            [FromBody] Product product,
            IProductDescriptionService descriptionService,
            IProductTranslationService translationService)
        => lang.Contains("English", StringComparison.OrdinalIgnoreCase)
            ? await descriptionService.GenerateProductDescription(product)
            : new(
                await translationService.Translate(
                    lang,
                    (await descriptionService.GenerateProductDescription(product)).Description)));

app.MapPost("/product-descriptions/translate/{lang=English}",
    async (
            [FromRoute] string lang,
            [FromBody] TranslationRequest request,
            IProductTranslationService translationService)
        =>
        await translationService.Translate(request)
    );

app.MapGet("/catalog/sample", async (ICatalogService svc) => await svc.SampleCatalog());
app.MapGet("/catalog", async (ICatalogService svc) => await svc.Get());
app.MapPost("/catalog", async (
    [FromBody]CatalogProduct[] products, ICatalogService svc) => await svc.Upload(products));

app.MapDefaultEndpoints();

app.Run();

internal static class ConfigExtensions
{
    public static IHttpClientBuilder ConfigureOpenAiHttpClient<TService>(this WebApplicationBuilder builder)
        where TService : class
    {
        var apiKey = builder.Configuration["OpenAI:ApiKey"]!;
        return builder.Services.AddHttpClient<TService>(client =>
        {
            client.BaseAddress = new Uri("https://api.openai.com/v1/completions");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        });
    }
}