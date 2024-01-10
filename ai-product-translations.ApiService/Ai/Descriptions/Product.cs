namespace AiProductTranslations.ApiService.Ai.Descriptions;

public record Product(
    string ProductName,
    string[] Features,
    string Color,
    string SizeRange,
    string Price);