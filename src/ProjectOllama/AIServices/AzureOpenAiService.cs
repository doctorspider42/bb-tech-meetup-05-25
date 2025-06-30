using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectOllama.AIServices;

/// <summary>
///     Service for interacting with Azure OpenAI API.
/// </summary>
public class AzureOpenAiService : IAiService
{
    private readonly string _apiKey;
    private readonly string _modelName;
    private readonly string _endpoint;
    private readonly HttpClient _httpClient;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AzureOpenAiService" /> class.
    /// </summary>
    /// <param name="endpoint">The Azure OpenAI endpoint URL.</param>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <param name="modelName">The model name to use for completions.</param>
    public AzureOpenAiService(string endpoint, string apiKey, string? modelName = null)
    {
        _httpClient = new HttpClient();
        _apiKey = apiKey ?? throw new ArgumentNullException(
            nameof(apiKey),
            "Azure OpenAI API key cannot be null");
        _endpoint = endpoint?.TrimEnd('/') ?? throw new ArgumentNullException(
            nameof(endpoint),
            "Azure OpenAI endpoint cannot be null");
        _modelName = modelName ?? throw new ArgumentNullException(
            nameof(modelName),
            "Model name must be provided in configuration");

        Console.WriteLine($"AzureOpenAiService initialized with endpoint: {_endpoint}");
        Console.WriteLine($"API key provided: {!string.IsNullOrEmpty(_apiKey)}");
        Console.WriteLine($"Using model: {_modelName}");
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AzureOpenAiService" /> class.
    /// </summary>
    /// <param name="httpClient">The HttpClient to use.</param>
    /// <param name="endpoint">The Azure OpenAI endpoint URL.</param>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <param name="modelName">The model name to use for completions.</param>
    public AzureOpenAiService(
        HttpClient httpClient,
        string endpoint,
        string apiKey,
        string? modelName = null)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _apiKey = apiKey ?? throw new ArgumentNullException(
            nameof(apiKey),
            "Azure OpenAI API key cannot be null");
        _endpoint = endpoint?.TrimEnd('/') ?? throw new ArgumentNullException(
            nameof(endpoint),
            "Azure OpenAI endpoint cannot be null");
        _modelName = modelName ?? throw new ArgumentNullException(
            nameof(modelName),
            "Model name must be provided in configuration");

        Console.WriteLine($"AzureOpenAiService initialized with endpoint: {_endpoint}");
        Console.WriteLine($"API key provided: {!string.IsNullOrEmpty(_apiKey)}");
        Console.WriteLine($"Using model: {_modelName}");
    }

    /// <summary>
    ///     Gets the name of the AI service provider.
    /// </summary>
    public string ProviderName => "Azure OpenAI";

    /// <summary>
    ///     Generates a completion using the specified model.
    /// </summary>
    /// <param name="modelName">The name of the model to use (deployment name in Azure).</param>
    /// <param name="prompt">The prompt to generate from.</param>
    /// <param name="temperature">The temperature to use for generation (0.0-1.0).</param>
    /// <returns>An AI completion result containing the generated text.</returns>
    public async Task<AiCompletionResult> GenerateCompletionAsync(
        string prompt,
        string? modelName,
        float temperature = 0.7f)
    {
        modelName = modelName ?? _modelName;

        var apiUrl =
            $"{_endpoint}/openai/deployments/{modelName}/chat/completions?api-version=2023-05-15";

        var request = new AzureOpenAiChatCompletionRequest
        {
            Messages = new[]
            {
                new ChatMessage
                {
                    Role = "user",
                    Content = prompt
                }
            },
            Temperature = temperature,
            MaxTokens = 800
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json");

        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl)
        {
            Content = content
        };

        httpRequestMessage.Headers.Add("api-key", _apiKey);

        var response = await _httpClient.SendAsync(httpRequestMessage);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var azureResponse =
            JsonSerializer.Deserialize<AzureOpenAiChatCompletionResponse>(responseContent) ??
            throw new InvalidOperationException(
                "Failed to deserialize the Azure OpenAI API response.");

        // Extract the text from the first choice (if available)
        var responseText = azureResponse.Choices.Length > 0
            ? azureResponse.Choices[0].Message.Content
            : string.Empty;

        return new AiCompletionResult
        {
            Text = responseText
        };
    }

    /// <summary>
    ///     Checks if the Azure OpenAI service is available and ready to accept requests.
    /// </summary>
    /// <returns>True if the service is available, otherwise false.</returns>
    public async Task<bool> IsAvailableAsync()
    {
        try
        {
            // Azure OpenAI doesn't have a dedicated health check endpoint,
            // but we can attempt to list models as a way to check connectivity
            var apiUrl = $"{_endpoint}/openai/models?api-version=2023-05-15";

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            httpRequestMessage.Headers.Add("api-key", _apiKey);

            var response = await _httpClient.SendAsync(httpRequestMessage);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    #region API Models

    /// <summary>
    ///     Azure OpenAI completion request model.
    /// </summary>
    private class AzureOpenAiCompletionRequest
    {
        [JsonPropertyName("prompt")] public string Prompt { get; set; } = string.Empty;

        [JsonPropertyName("temperature")] public float Temperature { get; set; }

        [JsonPropertyName("max_tokens")] public int MaxTokens { get; set; } = 800;
    }

    /// <summary>
    ///     Azure OpenAI chat completion request model.
    /// </summary>
    private class AzureOpenAiChatCompletionRequest
    {
        [JsonPropertyName("messages")]
        public ChatMessage[] Messages { get; set; } = Array.Empty<ChatMessage>();

        [JsonPropertyName("temperature")] public float Temperature { get; set; }

        [JsonPropertyName("max_tokens")] public int MaxTokens { get; set; } = 800;
    }

    /// <summary>
    ///     Chat message for Azure OpenAI chat completions API.
    /// </summary>
    private class ChatMessage
    {
        [JsonPropertyName("role")] public string Role { get; set; } = string.Empty;

        [JsonPropertyName("content")] public string Content { get; set; } = string.Empty;
    }

    /// <summary>
    ///     Azure OpenAI completion response model.
    /// </summary>
    private class AzureOpenAiCompletionResponse
    {
        [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

        [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

        [JsonPropertyName("created")] public long Created { get; set; }

        [JsonPropertyName("model")] public string Model { get; set; } = string.Empty;

        [JsonPropertyName("choices")] public Choice[] Choices { get; set; } = Array.Empty<Choice>();

        [JsonPropertyName("usage")] public Usage Usage { get; set; } = new();
    }

    /// <summary>
    ///     Azure OpenAI chat completion response model.
    /// </summary>
    private class AzureOpenAiChatCompletionResponse
    {
        [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

        [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

        [JsonPropertyName("created")] public long Created { get; set; }

        [JsonPropertyName("model")] public string Model { get; set; } = string.Empty;

        [JsonPropertyName("choices")]
        public ChatChoice[] Choices { get; set; } = Array.Empty<ChatChoice>();

        [JsonPropertyName("usage")] public Usage Usage { get; set; } = new();
    }

    /// <summary>
    ///     Represents a completion choice returned by Azure OpenAI.
    /// </summary>
    private class Choice
    {
        [JsonPropertyName("text")] public string Text { get; } = string.Empty;

        [JsonPropertyName("index")] public int Index { get; set; }

        [JsonPropertyName("finish_reason")] public string FinishReason { get; set; } = string.Empty;
    }

    /// <summary>
    ///     Represents a chat completion choice returned by Azure OpenAI.
    /// </summary>
    private class ChatChoice
    {
        [JsonPropertyName("message")] public ChatMessage Message { get; set; } = new();

        [JsonPropertyName("index")] public int Index { get; set; }

        [JsonPropertyName("finish_reason")] public string FinishReason { get; set; } = string.Empty;
    }

    /// <summary>
    ///     Represents the token usage information.
    /// </summary>
    private class Usage
    {
        [JsonPropertyName("prompt_tokens")] public int PromptTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }

        [JsonPropertyName("total_tokens")] public int TotalTokens { get; set; }
    }

    #endregion
}