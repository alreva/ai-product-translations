using ai_product_translations.Web;
using ai_product_translations.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Globalization / Localization
builder.Services.AddLocalization();

builder.AddRedisOutputCache("cache");

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddHttpClient<WeatherApiClient>(client =>
        client.BaseAddress = new("http://apiservice"));
builder.Services
    .AddHttpClient<CatalogApiClient>(client =>
        client.BaseAddress = new("http://apiservice"));
builder.Services
    .AddHttpClient<ProductDescriptionApiClient>(client =>
        client.BaseAddress = new("http://apiservice"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();

app.UseAntiforgery();

app.UseOutputCache();

var supportedCultures = new[] { "en-US", "es-CL", "uk-UA" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();
