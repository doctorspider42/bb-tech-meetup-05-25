namespace ProjectOllama.AIServices;

/// <summary>
///     Interface for AI completion services.
/// </summary>
public interface IAiService
{
    /// <summary>
    ///     Gets the name of the AI service provider.
    /// </summary>
    string ProviderName { get; }

    /// <summary>
    ///     Generates a completion using the specified model.
    /// </summary>
    /// <param name="modelName">The name of the model to use.</param>
    /// <param name="prompt">The prompt to generate from.</param>
    /// <param name="temperature">The temperature to use for generation (0.0-1.0).</param>
    /// <returns>An AI completion result containing the generated text.</returns>
    Task<AiCompletionResult> GenerateCompletionAsync(
        string prompt,
        string? modelName = null,
        float temperature = 0.7f);

    /// <summary>
    ///     Checks if the service is available and ready to accept requests.
    /// </summary>
    /// <returns>True if the service is available, otherwise false.</returns>
    Task<bool> IsAvailableAsync();
}

/// <summary>
///     Normalized AI completion result.
/// </summary>
public class AiCompletionResult
{
    /// <summary>
    ///     The generated text response.
    /// </summary>
    public string Text { get; set; } = string.Empty;
}