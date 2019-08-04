FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/ThermoTracker.Service/ThermoTracker.Service/ThermoTracker.Service.csproj", "src/ThermoTracker.Service/ThermoTracker.Service/"]
RUN dotnet restore "src/ThermoTracker.Service/ThermoTracker.Service/ThermoTracker.Service.csproj"
COPY . .
WORKDIR "/src/src/ThermoTracker.Service/ThermoTracker.Service"
RUN dotnet build "ThermoTracker.Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ThermoTracker.Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ThermoTracker.Service.dll"]