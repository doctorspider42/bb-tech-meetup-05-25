using ProjectOllama;
using ProjectOllama.AIServices;
using ProjectOllama.Messages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactDev", 
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddHttpClient();

// Register AI services based on configuration
var aiProvider = builder.Configuration.GetValue("AiProvider", AiProviders.Ollama);

switch (aiProvider)
{
    case AiProviders.AzureOpenAi:
        var azureOpenAiEndpoint = builder.Configuration["AzureOpenAi:Endpoint"];
        // Try to get the API key from user secrets first, then fall back to configuration
        var azureOpenAiKey = builder.Configuration["AzureOpenAi:ApiKey:Secret"] ?? 
                             builder.Configuration["AzureOpenAi:ApiKey"];

        if (!string.IsNullOrEmpty(azureOpenAiEndpoint) && !string.IsNullOrEmpty(azureOpenAiKey))
        {
            builder.Services.AddSingleton<IAiService>(provider => new AzureOpenAiService(
                provider.GetRequiredService<HttpClient>(),
                azureOpenAiEndpoint,
                azureOpenAiKey));

            Console.WriteLine("Using Azure OpenAI service");
        }
        else
        {
            throw new InvalidOperationException(
                "Azure OpenAI is enabled but endpoint or API key is missing in configuration or user secrets");
        }
        break;

    case AiProviders.DockerModel:
        // Register Docker Model service
        builder.Services.AddSingleton<IAiService, DockerModelService>();
        Console.WriteLine("Using Docker Model service");
        break;

    case AiProviders.Groq:
        var groqApiKey = builder.Configuration["Groq:ApiKey:Secret"] ?? 
                         builder.Configuration["Groq:ApiKey"];

        if (!string.IsNullOrEmpty(groqApiKey))
        {
            builder.Services.AddSingleton<IAiService>(provider => new GroqService(
                provider.GetRequiredService<HttpClient>(),
                groqApiKey));

            Console.WriteLine("Using Groq service");
        }
        else
        {
            throw new InvalidOperationException(
                "Groq is enabled but API key is missing in configuration or user secrets");
        }
        break;

    case AiProviders.Ollama:
    default:
        // Register Ollama service by default
        builder.Services.AddSingleton<IAiService, OllamaService>();
        Console.WriteLine("Using Ollama service");
        break;
}

builder.Services.AddSingleton<MailActionService>();
builder.Services.AddSingleton<ProductsDatabase>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("AllowReactDev");
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost(
    "ai/mail",
    async (MailActionService mailActionService, MailRequest request) =>
    {
        try
        {
            var response = await mailActionService.ProcessMail(request.EmailContent);
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error sending mail: {ex.Message}");
        }
    });

app.MapGet(
    "/ai/health",
    async (IServiceProvider serviceProvider) =>
    {
        var aiService = serviceProvider.GetRequiredService<IAiService>();
        var available = await aiService.IsAvailableAsync();

        return Results.Ok(
            new
            {
                status = available ? "healthy" : "unavailable",
                service = aiService.ProviderName,
                timestamp = DateTime.UtcNow
            });
    });

app.MapPost(
    "/ai/generate",
    async (IAiService aiService, CompletionRequest request) =>
    {
        try
        {
            var response = await aiService.GenerateCompletionAsync(
                request.Model,
                request.Prompt,
                request.Temperature);
            return Results.Ok(response);
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error generating completion: {ex.Message}");
        }
    });

app.Run();