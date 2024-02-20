namespace AiProductTranslations.ApiService.Ai.Descriptions;

public class OpenAiProductDescriptionService(OpenAiClient openAiClient) : IProductDescriptionService
{
    public async Task<ProductDescription> GenerateProductDescription(Product product)
    {
        const string context
            = """
              You are proficient in generating product descriptions.
              You pay special attention to the product features.           
              """;
        
        var prompt
            = $"""
              Product Name: {product.ProductName}
              Features: {string.Join(", ", product.Features)}
              Color: {product.Color}
              Size Range: {product.SizeRange}
              Price: {product.Price}
              """;
        var response = await openAiClient.ExecuteQuery(context, prompt);
        return new(response.Choices?.FirstOrDefault()?.Message?.Content ?? "");
    }
}