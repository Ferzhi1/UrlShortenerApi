FROM mcr.microsoft.com/dotnet/sdk:11.0 AS build
WORKDIR /src

COPY UrlShortener.Api/UrlShortener.Api.csproj .
RUN dotnet restore

COPY UrlShortener.Api .

RUN dotnet publish -c Release -o /app/publish /p:PublishFramework=true

FROM mcr.microsoft.com/dotnet/aspnet:11.0 AS final
WORKDIR /app

# Create non-root user

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

USER app:app
ENTRYPOINT ["dotnet", "UrlShortener.Api.dll"]
