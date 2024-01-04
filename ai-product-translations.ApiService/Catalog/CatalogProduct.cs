using Microsoft.Extensions.Primitives;

namespace AiProductTranslations.ApiService.Catalog;

public record CatalogProduct(
    string ProductName,
    string[] Features,
    string Color,
    string SizeRange,
    string Price,
    string Description = "");