namespace AiProductTranslations.ApiService.TranslationService;

public interface IProductDescriptionService
{
    Task<ProductDescription> GenerateProductDescription(Product product);
}