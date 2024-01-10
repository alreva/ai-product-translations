namespace AiProductTranslations.ApiService.Ai;

public record OpenAiResponse
{
    public Choice[]? Choices { get; set; }

    public record Choice
    {
        public string? Text { get; set; }
    }
}