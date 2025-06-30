namespace ProjectOllama.Messages;

/// <summary>
///     Normalized completion request for the AI services.
/// </summary>
public class CompletionRequest
{
    /// <summary>
    ///     The model name to use.
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    ///     The prompt to generate from.
    /// </summary>
    public string Prompt { get; set; } = string.Empty;

    /// <summary>
    ///     The temperature to use for generation (0.0-1.0).
    /// </summary>
    public float Temperature { get; set; } = 0.7f;
}