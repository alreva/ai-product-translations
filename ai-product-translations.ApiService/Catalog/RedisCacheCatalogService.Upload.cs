using System.Text.Json;

namespace AiProductTranslations.ApiService.Catalog;

partial class RedisCacheCatalogService : ICatalogService
{
    public Task Upload(CatalogProduct[] products)
    {
        return Task.CompletedTask;
    }
}