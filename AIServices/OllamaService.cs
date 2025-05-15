using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectOllama.AIServices;

public class OllamaService : IAiService
{
    private readonly HttpClient _httpClient;
    private readonly string _ollamaBaseUrl = "http://localhost:11434";
    private readonly string _configuredModel;

    public OllamaService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_ollamaBaseUrl);
        throw new ArgumentException("Model name must be provided in configuration");
    }

    public OllamaService(HttpClient httpClient, string? modelName = null)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_ollamaBaseUrl);
        _configuredModel = modelName ?? throw new ArgumentNullException(
            nameof(modelName),
            "Model name must be provided in configuration");
    }

    /// <summary>
    ///     Gets the name of the AI service provider.
    /// </summary>
    public string ProviderName => "Ollama";

    /// <summary>
    ///     Generates a completion using the specified model.
    /// </summary>
    /// <param name="modelName">The name of the model to use.</param>
    /// <param name="prompt">The prompt to generate from.</param>
    /// <param name="temperature">The temperature to use for generation (0.0-1.0).</param>
    /// <returns>An AI completion result containing the generated text.</returns>
    public async Task<AiCompletionResult> GenerateCompletionAsync(
        string prompt,
        string? modelName,
        float temperature = 0.7f)
    {
        var request = new OllamaCompletionRequest
        {
            Model = modelName ?? _configuredModel,
            Prompt = prompt,
            Temperature = temperature,
            Stream = false
        };

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/api/generate", content);
        response.EnsureSuccessStatusCode();

        var ollamaResponse = await response.Content.ReadFromJsonAsync<OllamaCompletionResponse>() ??
                             throw new InvalidOperationException(
                                 "Failed to deserialize the Ollama API response.");

        return new AiCompletionResult
        {
            Text = ollamaResponse.Response
        };
    }

    /// <summary>
    ///     Checks if the Ollama service is available and ready to accept requests.
    /// </summary>
    /// <returns>True if the service is available, otherwise false.</returns>
    public async Task<bool> IsAvailableAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/version");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}

#region API Models

public class OllamaModelListResponse
{
    [JsonPropertyName("models")]
    public OllamaModel[] Models { get; set; } = Array.Empty<OllamaModel>();
}

public class OllamaModel
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("model")] public string ModelFile { get; set; } = string.Empty;

    [JsonPropertyName("modified_at")] public string ModifiedAt { get; set; } = string.Empty;

    [JsonPropertyName("size")] public long Size { get; set; }
}

public class OllamaCompletionRequest
{
    [JsonPropertyName("model")] public string Model { get; set; } = string.Empty;

    [JsonPropertyName("prompt")] public string Prompt { get; set; } = string.Empty;

    [JsonPropertyName("temperature")] public float Temperature { get; set; } = 0.7f;

    [JsonPropertyName("stream")] public bool Stream { get; set; }
}

public class OllamaCompletionResponse
{
    [JsonPropertyName("model")] public string Model { get; set; } = string.Empty;

    [JsonPropertyName("response")] public string Response { get; set; } = string.Empty;

    [JsonPropertyName("done")] public bool Done { get; set; }

    [JsonPropertyName("total_duration")] public long TotalDuration { get; set; }

    [JsonPropertyName("load_duration")] public long LoadDuration { get; set; }

    [JsonPropertyName("prompt_eval_count")]
    public int PromptEvalCount { get; set; }

    [JsonPropertyName("prompt_eval_duration")]
    public long PromptEvalDuration { get; set; }

    [JsonPropertyName("eval_count")] public int EvalCount { get; set; }

    [JsonPropertyName("eval_duration")] public long EvalDuration { get; set; }
}

#endregion