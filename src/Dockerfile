# Use the official .NET Core SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-dev
EXPOSE 8080
EXPOSE 8081
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ../*.sln ./
COPY examservice.Api/*.csproj ./examservice.Api/
COPY examservice.Core/*.csproj ./examservice.Core/
COPY examservice.Domain/*.csproj ./examservice.Domain/
COPY examservice.Infrastructure/*.csproj ./examservice.Infrastructure/
COPY examservice.Service/*.csproj ./examservice.Service/

# Restore dependencies
RUN dotnet restore "examservice.Api/examservice.Api.csproj"

# Copy everything else and build
COPY . ./

# Specify the project to build
WORKDIR /src/examservice.Api
RUN dotnet build examservice.Api.csproj -c Release -o out

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build-dev /src/examservice.Api/out ./
ENTRYPOINT ["dotnet", "examservice.Api.dll"]
