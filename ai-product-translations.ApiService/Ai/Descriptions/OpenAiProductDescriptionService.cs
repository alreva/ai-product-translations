namespace AiProductTranslations.ApiService.Ai.Descriptions;

public class OpenAiProductDescriptionService(OpenAiClient openAiClient) : IProductDescriptionService
{
    public async Task<ProductDescription> GenerateProductDescription(Product product)
    {
        var prompt = $"""
                      Write a product description for the following item:
                      Product Name: {product.ProductName}
                      Features: {string.Join(", ", product.Features)}
                      Color: {product.Color}
                      Size Range: {product.SizeRange}
                      Price: {product.Price}
                      Focus on highlighting the key features and appeal of the product.
                      """;
        var response = await openAiClient.ExecuteQuery(prompt);
        return new(response.Choices?.FirstOrDefault()?.Text ?? "");
    }
}