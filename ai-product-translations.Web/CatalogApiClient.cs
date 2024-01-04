namespace ai_product_translations.Web;

public class CatalogApiClient(HttpClient http)
{
    public async Task<CatalogProductViewModel[]> GetProducts()
    {
        return await http.GetFromJsonAsync<CatalogProductViewModel[]>("/catalog") ?? [];
    }
}

public record CatalogProductViewModel(
    string ProductName,
    string[] Features,
    string Color,
    string SizeRange,
    string Price,
    string? Description = null);