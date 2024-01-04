namespace AiProductTranslations.ApiService.Catalog;

public interface ICatalogService
{
    Task<CatalogProduct[]> SampleCatalog();
    Task<CatalogProduct[]> Get();
    Task Upload(CatalogProduct[] products);
}