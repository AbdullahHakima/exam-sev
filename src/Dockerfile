# Use the official .NET Core SDK image as a build stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
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
RUN dotnet build examservice.Api.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish examservice.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "examservice.Api.dll"]
