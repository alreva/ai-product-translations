namespace AiProductTranslations.ApiService.TranslationService;

class OpenAiProductDescriptionService(
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration,
    ILogger<OpenAiProductDescriptionService> logger)
    : IProductDescriptionService
{
    private readonly string _apiKey = configuration["OpenAI:ApiKey"] ?? "";

    public async Task<ProductDescription> GenerateProductDescription(Product product)
    {
        logger.LogInformation("Using API Key {ApiKey}", _apiKey);
        using var httpClient = httpClientFactory.CreateClient("openAI");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        var request = new
        {
            model = "gpt-3.5-turbo-instruct", // or any other model
            prompt = GeneratePrompt(product),
            temperature = 0.7,
            max_tokens = 800
        };
        logger.LogInformation("Request data: {@Request}", request);

        using var response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/completions", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<OpenAiResponse>();
        logger.LogInformation("Response data: {@Response}", result);

        return new(result?.Choices?.FirstOrDefault()?.Text ?? "");
    }

    private static string GeneratePrompt(Product product)
    {
        var features = string.Join(", ", product.Features);
        return $"""
                Write a product description for the following item:
                Product Name: {product.ProductName}
                Features: {features}
                Color: {product.Color}
                Size Range: {product.SizeRange}
                Price: {product.Price}
                Focus on highlighting the key features and appeal of the product.
                """;
    }

    private record OpenAiResponse
    {
        public Choice[]? Choices { get; set; }

        public record Choice
        {
            public string? Text { get; set; }
        }
    }
}