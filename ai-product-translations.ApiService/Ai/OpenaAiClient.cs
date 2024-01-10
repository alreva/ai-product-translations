using System.Text.Json;

namespace AiProductTranslations.ApiService.Ai;

public class OpenAiClient(HttpClient httpClient, ILogger<OpenAiClient> logger)
{
    public async Task<OpenAiResponse> ExecuteQuery(string prompt)
    {
        logger.LogInformation("Base address: {BaseAddress}", httpClient.BaseAddress);
        var requestData = new
        {
            model = "gpt-3.5-turbo-instruct", // or any other model
            prompt,
            temperature = 0.7,
            max_tokens = 800
        };
        logger.LogInformation("Request data: {@Request}", requestData);
        using var httpResponse = await httpClient.PostAsJsonAsync("/v1/completions", requestData);
        httpResponse.EnsureSuccessStatusCode();
        var response = (await httpResponse.Content.ReadFromJsonAsync<OpenAiResponse>())!;
        logger.LogInformation(
            "Response data: {@Response}",
            JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));
        return response;
    }
}