namespace AiProductTranslations.ApiService.Ai.Translations;

public interface IProductTranslationService
{
    async Task<string> Translate(string language, string content)
        => (await Translate(new TranslationRequest(new Language(language), content))).Content;
    Task<TranslationResult> Translate(TranslationRequest request);
}