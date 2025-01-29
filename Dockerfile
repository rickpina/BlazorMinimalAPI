# Use the official .NET 9 ASP.NET runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET 9 SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore any dependencies (via dotnet restore)
COPY ["BlazorMinimalAPI.csproj", "./"]
RUN dotnet restore "./BlazorMinimalAPI.csproj"

# Copy the rest of the code and build the app
COPY . .
RUN dotnet build "BlazorMinimalAPI.csproj" -c Release -o /app/build

# Publish the app to the /app/publish directory
FROM build AS publish
RUN dotnet publish "BlazorMinimalAPI.csproj" -c Release -o /app/publish

# Final stage: Copy the published app to the base image and define entry point
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorMinimalAPI.dll"]
