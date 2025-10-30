# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 2122
EXPOSE 2122


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /Backend
COPY ["MisTrace.ApiService/MisTrace.ApiService.csproj", "MisTrace.ApiService/"]
COPY ["MisTrace.Application/MisTrace.Application.csproj", "MisTrace.Application/"]
COPY ["MisTrace.Domain/MisTrace.Domain.csproj", "MisTrace.Domain/"]
COPY ["MisTrace.Infrastructure/MisTrace.Infrastructure.csproj", "MisTrace.Infrastructure/"]
RUN dotnet restore "./MisTrace/MisTrace.ApiService.csproj"
COPY . .
WORKDIR "/Backend/MisTrace.ApiService"
RUN dotnet build "./MisTrace.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MisTrace.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["sh", "-c", "dotnet MisTrace.ApiService.dll"]