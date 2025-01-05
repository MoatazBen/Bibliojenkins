# Use the official .NET 6 SDK image to build and publish the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the entire application source code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o /out

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /out .

# Expose the port the application listens on
EXPOSE 80
EXPOSE 443

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "YourAppName.dll"]
