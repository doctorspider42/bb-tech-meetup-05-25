namespace ProjectOllama;

/// <summary>
///     Enumeration of available AI service providers.
/// </summary>
public enum AiProviders
{
    /// <summary>
    ///     Use Ollama as the AI service provider.
    /// </summary>
    Ollama,

    /// <summary>
    ///     Use Azure OpenAI as the AI service provider.
    /// </summary>
    AzureOpenAi,

    /// <summary>
    ///     Use Docker Model as the AI service provider.
    /// </summary>
    DockerModel,

    /// <summary>
    ///     Use Groq as the AI service provider.
    /// </summary>
    Groq
}