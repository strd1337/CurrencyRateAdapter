FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CurrencyRateAdapter.Presentation/CurrencyRateAdapter.Presentation.csproj", "src/CurrencyRateAdapter.Presentation/"]
RUN dotnet restore "./src/CurrencyRateAdapter.Presentation/CurrencyRateAdapter.Presentation.csproj"
COPY . .
WORKDIR "/src/src/CurrencyRateAdapter.Presentation"
RUN dotnet build "./CurrencyRateAdapter.Presentation.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CurrencyRateAdapter.Presentation.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CurrencyRateAdapter.Presentation.dll"]
