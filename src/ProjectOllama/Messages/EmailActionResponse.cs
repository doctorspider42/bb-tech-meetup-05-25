using System.Text.Json.Serialization;

namespace ProjectOllama.Messages;

public class EmailActionResponse
{
    [JsonPropertyName("tool")]
    public string Tool { get; set; } = string.Empty;

    [JsonPropertyName("details")]
    public string Details { get; set; } = string.Empty;

    [JsonPropertyName("justification")]
    public string Justification { get; set; } = string.Empty;

    [JsonPropertyName("totalTime")]
    public string TotalTime { get; set; } = string.Empty;
}