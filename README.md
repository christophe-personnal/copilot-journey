# copilot-journey
Sample repository demonstrating several features of GitHub Copilot.

## Overview
In this sample repository you'll find work demonstrating various GitHub Copilot features.
The sample is an API built on the .NET stack. Examples include:
- Documentation generation with autocompletion
- Project initialization
- Using instructions to guide Copilot
- Using chat modes

## Sample API
The sample API is a simple Weather Forecast API that exposes endpoints to retrieve weather data:
- Current weather data
- 7-day weather forecast
- Weather alerts

The weather data is retrieved from an open-source API: https://open-meteo.com/.

The API is built using the ASP.NET Core Web API framework.

The project structure initially follows Copilot's natural suggestions and is then adjusted to follow best practices for maintainability and scalability. This README should not overly influence suggestions.

We'll create the initial commits manually, then ask Copilot to generate commit messages for subsequent commits.

## Run the API locally

To build and run the sample API locally (requires .NET 9 SDK):

```powershell
# from repository root
dotnet restore
dotnet build
dotnet run --project src/CopilotJourney.App/CopilotJourney.App.csproj
```

When the app starts it will listen on the default URLs and expose Swagger in development mode. Use a browser or curl to call the endpoints:

 - GET /api/WeatherForecast/current
 - GET /api/WeatherForecast/forecast
 - GET /api/WeatherForecast/alerts

