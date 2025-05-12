# Email UI React App

This is a modern React application for the Project Ollama AI email assistant.

## Getting Started

To set up and run the React application:

1. Navigate to the EmailUI directory:
   ```
   cd EmailUI
   ```

2. Install the dependencies:
   ```
   npm install
   ```

3. Run the development server:
   ```
   npm start
   ```

This will start the React development server on port 3000, with API requests proxied to the .NET backend.

## Building for Production

To build the application for production:

```
npm run build
```

To build the app and automatically copy it to the .NET project's wwwroot directory:

```
npm run build:deploy
```

To only copy the existing build files to wwwroot without rebuilding:

```
npm run copy-build
```

The .NET project is configured to automatically build and include the React app when publishing.

## Features

- Modern UI with styled components
- Email input form with character counter
- Real-time AI analysis of email content
- Responsive design for all device sizes
