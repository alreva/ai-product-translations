namespace AiProductTranslations.ApiService.Ai.Translations;

public record Language(string NaturalLanguageName)
{
    public override string ToString() => NaturalLanguageName;
}