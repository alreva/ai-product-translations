using System.Text.Json;

namespace AiProductTranslations.ApiService;

public class JsonOptions
{
    public static readonly JsonSerializerOptions Default = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}