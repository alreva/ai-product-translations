namespace AiProductTranslations.ApiService.Ai.Descriptions;

public interface IProductDescriptionService
{
    Task<ProductDescription> GenerateProductDescription(Product product);
}