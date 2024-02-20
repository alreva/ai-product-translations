namespace AiProductTranslations.ApiService.Ai;

public record OpenAiResponse(Choice[]? Choices);

public record Choice(Message? Message);

public record Message(string? Content);
