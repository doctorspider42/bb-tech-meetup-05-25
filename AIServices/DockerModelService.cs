using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectOllama.AIServices;

/// <summary>
///     Service for interacting with Docker-hosted model API.
/// </summary>
public class DockerModelService : IAiService
{
    private readonly HttpClient _httpClient;
    private readonly string _dockerModelBaseUrl = "http://localhost:12434";
    private readonly string _configuredModel;

    public DockerModelService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_dockerModelBaseUrl);
        throw new ArgumentException("Model name must be provided in configuration");
    }

    public DockerModelService(HttpClient httpClient, string? modelName = null)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_dockerModelBaseUrl);
        _configuredModel = modelName ?? throw new ArgumentNullException(
            nameof(modelName),
            "Model name must be provided in configuration");
        Console.WriteLine($"DockerModelService initialized with model: {_configuredModel}");
    }

    /// <summary>
    ///     Gets the name of the AI service provider.
    /// </summary>
    public string ProviderName => "Docker Model";

    /// <summary>
    ///     Generates a completion using the specified model.
    /// </summary>
    /// <param name="prompt">The prompt to generate from.</param>
    /// <param name="modelName">The name of the model to use.</param>
    /// <param name="temperature">The temperature to use for generation (0.0-1.0).</param>
    /// <returns>An AI completion result containing the generated text.</returns>
    public async Task<AiCompletionResult> GenerateCompletionAsync(
        string prompt,
        string? modelName,
        float temperature = 0.7f)
    {
        var request = new DockerModelChatCompletionRequest
        {
            Model = modelName ?? _configuredModel,
            Messages = new[]
            {
                new Message
                {
                    Role = "user",
                    Content = prompt
                }
            },
            Temperature = temperature,
            MaxTokens = 800
        };

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/engines/llama.cpp/v1/chat/completions", content);
        response.EnsureSuccessStatusCode();

        var dockerResponse = await response.Content.ReadFromJsonAsync<DockerModelChatCompletionResponse>() ??
                            throw new InvalidOperationException(
                                "Failed to deserialize the Docker Model API response.");

        return new AiCompletionResult
        {
            Text = dockerResponse.Choices.Length > 0 ? dockerResponse.Choices[0].Message.Content : string.Empty
        };
    }

    /// <summary>
    ///     Checks if the Docker Model service is available and ready to accept requests.
    /// </summary>
    /// <returns>True if the service is available, otherwise false.</returns>
    public async Task<bool> IsAvailableAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/health");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}

#region API Models

public class DockerModelChatCompletionRequest
{
    [JsonPropertyName("model")] public string Model { get; set; } = string.Empty;

    [JsonPropertyName("messages")] public Message[] Messages { get; set; } = Array.Empty<Message>();

    [JsonPropertyName("temperature")] public float Temperature { get; set; } = 0.7f;

    [JsonPropertyName("max_tokens")] public int MaxTokens { get; set; } = 800;
}

public class Message
{
    [JsonPropertyName("role")] public string Role { get; set; } = string.Empty;

    [JsonPropertyName("content")] public string Content { get; set; } = string.Empty;
}

public class DockerModelChatCompletionResponse
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("created")] public long Created { get; set; }

    [JsonPropertyName("model")] public string Model { get; set; } = string.Empty;

    [JsonPropertyName("choices")] public ChatChoice[] Choices { get; set; } = Array.Empty<ChatChoice>();

    [JsonPropertyName("usage")] public Usage Usage { get; set; } = new();
}

public class ChatChoice
{
    [JsonPropertyName("message")] public Message Message { get; set; } = new();

    [JsonPropertyName("index")] public int Index { get; set; }

    [JsonPropertyName("finish_reason")] public string FinishReason { get; set; } = string.Empty;
}

public class Usage
{
    [JsonPropertyName("prompt_tokens")] public int PromptTokens { get; set; }

    [JsonPropertyName("completion_tokens")] public int CompletionTokens { get; set; }

    [JsonPropertyName("total_tokens")] public int TotalTokens { get; set; }
}

#endregion
