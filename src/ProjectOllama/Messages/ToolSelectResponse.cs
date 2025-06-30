using System.Text.Json.Serialization;

namespace ProjectOllama.Messages;

public class ToolSelectResponse
{
    [JsonPropertyName("tool")]
    public string Tool { get; set; } = string.Empty;

    [JsonPropertyName("details")]
    public string Details { get; set; } = string.Empty;

    [JsonPropertyName("product_mentioned")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProductMentioned { get; set; }
}