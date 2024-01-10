using Microsoft.Extensions.Caching.Distributed;

namespace AiProductTranslations.ApiService.Ai.Translations;

public class RedisCacheDelegatingProductTranslationService(
    IProductTranslationService wrapped,
    IDistributedCache cache
) : RedisCachingServiceDecorator<TranslationRequest, TranslationResult>(cache),
    IProductTranslationService
{
    protected override Task<TranslationResult> CallService(TranslationRequest request)
        => wrapped.Translate(request);

    public Task<TranslationResult> Translate(TranslationRequest product)
        => CallServiceOrGetFromCache(product);
}