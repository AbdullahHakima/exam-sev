# Use the official .NET Core SDK image as a build stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Install the latest version of font libraries
RUN apt update && \
    apt install -y --no-install-recommends \
        libc6 \
        libicu-dev \
        libfontconfig1 && \
    rm -rf /var/lib/apt/lists/*

RUN echo "deb http://ftp.debian.org/debian/ bullseye contrib" >> /etc/apt/sources.list && \
    apt update && \
    apt install -y --no-install-recommends \
        ttf-mscorefonts-installer && \
    rm -rf /var/lib/apt/lists/*

# Build frontend
FROM node:18 AS build-frontend
WORKDIR /app
COPY examservice.Api/package.json ./
RUN npm install

# Build .NET application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

RUN dotnet nuget add source https://nuget.devexpress.com/YwwelfDaxKEGUaMzaZ5aU4dR8OVZlnmgKgfox53e3K2bqpZcnS/api

# Copy csproj and restore as distinct layers
COPY *.sln ./
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

# Create node_modules directory and copy frontend dependencies
RUN mkdir -p /app/node_modules
COPY --from=build-frontend /app/node_modules /app/node_modules/

# Publish the .NET application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish examservice.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /app/node_modules /app/node_modules/

ENTRYPOINT ["dotnet", "examservice.Api.dll"]
