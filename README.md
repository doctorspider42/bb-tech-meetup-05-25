# Project Ollama

Project Ollama is an AI-powered email assistant built with **.NET 9** and a React front-end. It exposes a small HTTP API that can classify incoming emails, generate responses and interact with multiple AI providers.

## Requirements

- .NET 9 SDK
- Node.js 18+

## Repository structure

The solution file `ProjectOllama.sln` lives in the repository root. Source code
is located under `src/` and unit tests under `tests/`:

- `src/ProjectOllama` – ASP.NET Core backend project
- `tests/ProjectOllama.Tests` – xUnit test project


## Configuration

1. Use `dotnet user-secrets` to store your API keys outside the repository. The file `secrets.example.json` shows the required structure. Example:
   ```bash
   dotnet user-secrets set "AzureOpenAi:ApiKey" "<your-key>"
   dotnet user-secrets set "AzureOpenAi:Endpoint" "<your-endpoint>"
   ```
2. Edit `src/ProjectOllama/appsettings.json` to select the AI provider via the `AiProvider` option. Supported providers are `Ollama`, `AzureOpenAi`, `Groq` and `DockerModel`. Each provider has its own section with model names and connection details.

## Running the application

### Development

Run the API and the React UI separately:

```bash
# start the ASP.NET backend
dotnet run --project src/ProjectOllama
```

In another terminal:

```bash
cd EmailUI
npm install
npm start
```

The React dev server proxies API calls to `http://localhost:5130`.

### Production build

To build the React UI and serve it from ASP.NET:

```bash
cd EmailUI
npm install
npm run build:deploy
cd ..
dotnet run --project src/ProjectOllama
```

Static files will be copied to `wwwroot` and hosted by the backend.

## API Endpoints

- `POST /ai/mail` – Analyze an email and return a JSON action.
- `GET /ai/health` – Check the selected AI provider status.
- `POST /ai/generate` – General completion endpoint with `prompt`, `model` and `temperature` fields.

Example request:

```bash
curl -X POST http://localhost:5130/ai/mail \
  -H "Content-Type: application/json" \
  -d '{"emailContent":"Your email text"}'
```

See [EmailUI/README.md](EmailUI/README.md) for details on the front‑end project.
