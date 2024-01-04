using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace AiProductTranslations.ApiService.TranslationService;

public class RedisCacheDelegatingProductDescriptionService(
    IProductDescriptionService wrapped,
    IDistributedCache cache
    ) : IProductDescriptionService
{
    public async Task<ProductDescription> GenerateProductDescription(Product product)
    {
        return
            await TryGetFromCache(product)
            ?? await Cache(product, await wrapped.GenerateProductDescription(product));
    }
    
    private async Task<ProductDescription> Cache(Product product, ProductDescription productDescription)
    {
        await cache.SetAsync(BuildKey(product), JsonSerializer.SerializeToUtf8Bytes(productDescription));
        return productDescription;
    }

    private async Task<ProductDescription?> TryGetFromCache(Product product)
    {
        var val = await cache.GetAsync(BuildKey(product));
        return val == null ? null : JsonSerializer.Deserialize<ProductDescription>(val);
    }

    private static string BuildKey(Product product)
    {
        return JsonSerializer.Serialize(product);
    }
}