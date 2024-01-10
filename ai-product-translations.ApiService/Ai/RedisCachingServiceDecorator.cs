using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace AiProductTranslations.ApiService.Ai;

public abstract class RedisCachingServiceDecorator<TRequest, TResult>(
    IDistributedCache cache
    )
{
    protected abstract Task<TResult> CallService(TRequest request);
    
    public async Task<TResult> CallServiceOrGetFromCache(TRequest request)
    {
        return
            await TryGetFromCache(request)
            ?? await Cache(request, await CallService(request));
    }
    
    private async Task<TResult> Cache(TRequest request, TResult result)
    {
        await cache.SetAsync(BuildKey(request), JsonSerializer.SerializeToUtf8Bytes(result));
        return result;
    }

    private async Task<TResult?> TryGetFromCache(TRequest request)
    {
        var val = await cache.GetAsync(BuildKey(request));
        return val == null ? default : JsonSerializer.Deserialize<TResult>(val);
    }

    private static string BuildKey(TRequest request)
    {
        return JsonSerializer.Serialize(request);
    }
}