using Microsoft.Extensions.Caching.Distributed;

namespace AiProductTranslations.ApiService.Ai.Descriptions;

public class RedisCachingProductDescriptionService(
    IProductDescriptionService wrapped,
    IDistributedCache cache
) : RedisCachingServiceDecorator<Product, ProductDescription>(cache),
    IProductDescriptionService
{
    protected override Task<ProductDescription> CallService(Product request)
        => wrapped.GenerateProductDescription(request);

    public Task<ProductDescription> GenerateProductDescription(Product product)
        => CallServiceOrGetFromCache(product);
}