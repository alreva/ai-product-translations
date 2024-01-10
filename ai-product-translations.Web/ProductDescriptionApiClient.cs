using System.Globalization;

namespace ai_product_translations.Web;

public class ProductDescriptionApiClient(HttpClient http)
{
    public async Task<ProductDescriptionOutputViewModel> GetDescription(ProductDescriptionInputViewModel input)
    {
        var response = await http.PostAsJsonAsync($"/product-descriptions/gen/{input.Lang}", input);
        return await response.Content.ReadFromJsonAsync<ProductDescriptionOutputViewModel>()
               ?? new ProductDescriptionOutputViewModel();
    }

    public async Task<ProductTranslationResultViewModel> Translate(ProductTranslationRequestViewModel input)
    {
        var response = await http.PostAsJsonAsync($"/product-descriptions/translate/{input.Language}", input);
        return await response.Content.ReadFromJsonAsync<ProductTranslationResultViewModel>()
               ?? new ProductTranslationResultViewModel("");
    }
}

public record LanguageViewModel(string NaturalLanguageName)
{
    public override string ToString() => NaturalLanguageName;
}

public record ProductTranslationRequestViewModel(LanguageViewModel Language, string Content);
public record ProductTranslationResultViewModel(string Content);

public record ProductDescriptionInputViewModel(
    string Lang,
    string ProductName,
    string[] Features,
    string Color,
    string SizeRange,
    string Price);
    
public record ProductDescriptionOutputViewModel(string Description = "");