namespace AiProductTranslations.ApiService.TranslationService;

public record Product(
    string ProductName,
    string[] Features,
    string Color,
    string SizeRange,
    string Price);