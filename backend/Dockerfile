# Use the official .NET 9 SDK image because this stage restores, builds, and publishes C# code.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working folder inside the container for all build commands.
WORKDIR /src

# Copy the API project file first so Docker can cache NuGet restore when source files change.
COPY backend/EmployeeHub.Api.csproj backend/

# Restore NuGet packages required by the ASP.NET Core API.
RUN dotnet restore backend/EmployeeHub.Api.csproj

# Copy the backend source code into the build container.
COPY backend/ backend/

# Publish the API into a small output folder that the runtime image can use.
RUN dotnet publish backend/EmployeeHub.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# Use the smaller ASP.NET Core runtime image because the app is already compiled.
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Set the folder where the running API files will live.
WORKDIR /app

# Tell Docker that the API listens on port 8080 inside the container.
EXPOSE 8080

# Copy only the published API files from the build stage.
COPY --from=build /app/publish .

# Start the ASP.NET Core API when the container runs.
ENTRYPOINT ["dotnet", "EmployeeHub.Api.dll"]
