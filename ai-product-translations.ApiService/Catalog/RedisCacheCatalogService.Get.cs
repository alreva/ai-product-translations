using System.Text.Json;

namespace AiProductTranslations.ApiService.Catalog;

partial class RedisCacheCatalogService : ICatalogService
{
    public async Task<CatalogProduct[]> Get()
    {
        return await SampleCatalog();
    }
}