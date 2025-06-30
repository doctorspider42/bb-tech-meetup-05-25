namespace ProjectOllama.Messages;

/// <summary>
/// Represents a request to process an email by the AI service.
/// </summary>
public class MailRequest
{
    /// <summary>
    /// Gets or sets the email content to be processed by the AI.
    /// </summary>
    public required string EmailContent { get; set; }
}
