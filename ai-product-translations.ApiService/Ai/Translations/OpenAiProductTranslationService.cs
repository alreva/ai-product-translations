namespace AiProductTranslations.ApiService.Ai.Translations;

public class OpenAiProductTranslationService(OpenAiClient openAiClient) : IProductTranslationService
{
    public async Task<TranslationResult> Translate(TranslationRequest request)
    {
        var context
            = $"""
              You are a product translation service, translating the text from en-US to {request.Language}.
              """;
        var response = await openAiClient.ExecuteQuery(context, request.content);
        return new(request.Language, response.Choices?.FirstOrDefault()?.Message?.Content ?? "");
    }
}