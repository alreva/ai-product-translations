using System.Text.Json;

namespace AiProductTranslations.ApiService.Ai;

public class OpenAiClient(HttpClient httpClient, ILogger<OpenAiClient> logger)
{
    public async Task<OpenAiResponse> ExecuteQuery(string context, string prompt)
    {
        logger.LogInformation("Base address: {BaseAddress}", httpClient.BaseAddress);
        
        var requestData = new
        {
            model = "gpt-3.5-turbo", // or any other model
            messages = new object[] {
                new {role = "system", content = context},
                new {role = "user", content = prompt},
            },
            temperature = 0.7,
            max_tokens = 800
        };
        
        logger.LogInformation("Request data: {@Request}", JsonSerializer.Serialize(requestData));
        using var httpResponse = await httpClient.PostAsJsonAsync("", requestData);
        httpResponse.EnsureSuccessStatusCode();
        var response = (await httpResponse.Content.ReadFromJsonAsync<OpenAiResponse>())!;
        logger.LogInformation(
            "Response data: {@Response}",
            JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }));
        return response;
    }
}