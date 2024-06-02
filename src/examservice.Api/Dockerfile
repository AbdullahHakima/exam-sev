# Use the official .NET Core SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-dev
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY ["src/examservice.Api/*.csproj", "./src/examservice.Api/"]
COPY ["src/examservice.Core/*.csproj" ,"./src/examservice.Core/"]
COPY ["src/examservice.Domain/*.csproj" ,"./src/examservice.Domain/"]
COPY ["src/examservice.Infrastructure/*.csproj", "./src/examservice.Infrastructure/"]
COPY ["src/examservice.Service/*.csproj ","./src/examservice.Service/"]

RUN dotnet restore "examservice.Api/*.csproj ./examservice.Api/"
RUN dotnet restore "examservice.Core/*.csproj ./examservice.Core/"
RUN dotnet restore "examservice.Domain/*.csproj ./examservice.Domain/"
RUN dotnet restore "examservice.Infrastructure/*.csproj ./examservice.Infrastructure/"
RUN dotnet restore "examservice.Service/*.csproj ./examservice.Service/"


# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build-dev /src/out ./
ENTRYPOINT ["dotnet", "examservice.Api.dll"]
