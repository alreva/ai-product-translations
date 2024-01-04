namespace ai_product_translations.Web;

public class ProductDescriptionApiClient(HttpClient http)
{
    public async Task<ProductDescriptionOutputViewModel> GetDescription(ProductDescriptionInputViewModel product)
    {
        var response = await http.PostAsJsonAsync("/product-descriptions/gen", product);
        return await response.Content.ReadFromJsonAsync<ProductDescriptionOutputViewModel>()
               ?? new ProductDescriptionOutputViewModel();
    }
}

public record ProductDescriptionInputViewModel(
    string ProductName,
    string[] Features,
    string Color,
    string SizeRange,
    string Price);
    
public record ProductDescriptionOutputViewModel(string Description = "");