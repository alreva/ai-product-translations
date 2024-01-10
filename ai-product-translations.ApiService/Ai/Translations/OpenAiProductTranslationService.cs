namespace AiProductTranslations.ApiService.Ai.Translations;

public class OpenAiProductTranslationService(OpenAiClient openAiClient) : IProductTranslationService
{
    public async Task<TranslationResult> Translate(TranslationRequest request)
    {
        var prompt = $"""
                      Please translate the following text to {request.Language}:
                      {request.content}
                      """;
        var response = await openAiClient.ExecuteQuery(prompt);
        return new(request.Language, response.Choices?.FirstOrDefault()?.Text ?? "");
    }
}