using ProjectOllama.AIServices;
using ProjectOllama.Messages;
using System.Text.Json;

namespace ProjectOllama;

public class MailActionService(IAiService aiService)
{
    public async Task<EmailActionResponse?> ProcessMail(string emailText)
    {
        // Start the timer to measure total processing time
        var startTime = DateTime.Now;

        var promptTemplate = await File.ReadAllTextAsync("Prompts/ToolSelectPrompt.txt");
        var fullPrompt = promptTemplate + "\n\nEmail Content:\n" + emailText;

        var result = await aiService.GenerateCompletionAsync(fullPrompt);

        try
        {
            var response = JsonSerializer.Deserialize<ToolSelectResponse>(result.Text);

            // Ensure we have a valid response (not null)
            if (response == null)
                throw new InvalidOperationException("Failed to deserialize AI response");

            EmailActionResponse emailActionResponse;

            switch (response.Tool)
            {
                case "cooperation_opportunity":
                    emailActionResponse = new EmailActionResponse
                    {
                        Tool = "cooperation_opportunity",
                        Details = "redirected to marketing team",
                        Justification = response.Details
                    };
                    //Redirect email to marketing team
                    break;
                case "product_question":
                    var productName = response.ProductMentioned ?? "unknown product";
                    var productResponse = await AnswerAboutProduct(productName, emailText);
                    emailActionResponse = new EmailActionResponse
                    {
                        Tool = "product_question",
                        Details = productResponse,
                        Justification = response.Details
                    };
                    break;
                case "it_support_ticket":
                    var supportEmail = await WriteEmailToSupport(emailText);
                    emailActionResponse = new EmailActionResponse
                    {
                        Tool = "it_support_ticket",
                        Details = supportEmail,
                        Justification = response.Details
                    };
                    break;
                case "spam":
                default:
                    emailActionResponse = new EmailActionResponse
                    {
                        Tool = "spam",
                        Details = "filtered as spam",
                        Justification = response.Details
                    };
                    // Mark as spam
                    break;
            } // Calculate total time in seconds and add to the response

            var totalTime = (DateTime.Now - startTime).TotalSeconds;
            emailActionResponse.TotalTime = $"{totalTime:0.00} seconds";

            return emailActionResponse;
        }
        catch (JsonException)
        {
            // If JSON deserialization fails, return a basic response with the raw text
            var totalTime = (DateTime.Now - startTime).TotalSeconds;
            return new EmailActionResponse
            {
                Tool = "error",
                Details =
                    $"Could not parse AI response as expected format. Raw response: {result.Text}",
                Justification = "Error occurred during processing",
                TotalTime = $"{totalTime:0.00} seconds"
            };
        }
    }

    private async Task<string> WriteEmailToSupport(string emailText)
    {
        var promptTemplate = await File.ReadAllTextAsync("Prompts/SupportEmailPrompt.txt");
        var fullPrompt = promptTemplate + "\n\nEmail Content:\n" + emailText;

        var result = await aiService.GenerateCompletionAsync(fullPrompt);
        return result.Text;
    }

    private async Task<string> AnswerAboutProduct(string productName, string emailText)
    {
        // Find the product information using the ProductsDatabase
        var productInfo = ProductsDatabase.Find(productName);

        if (productInfo == null)
            // If product not found, return a generic response
            return
                "We couldn't find specific information about the product mentioned. Please provide more details about which product you're inquiring about.";

        // Load the product email prompt template
        var promptTemplate = await File.ReadAllTextAsync("Prompts/ProductEmailPrompt.txt");

        // Create the full prompt with product information and email text
        var fullPrompt = promptTemplate + "\n\n" + productInfo + "\n\nCustomer Email:\n" +
                         emailText;

        // Generate the response using the AI service
        var result = await aiService.GenerateCompletionAsync(fullPrompt);
        return result.Text;
    }
}