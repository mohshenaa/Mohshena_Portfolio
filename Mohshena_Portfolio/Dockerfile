# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 8080

# Copy published files from build stage
COPY --from=build /app/publish .

# Set environment variable for Render's port
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Mohshena_Portfolio.dll"]