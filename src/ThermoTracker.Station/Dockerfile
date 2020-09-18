FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

COPY bin/Release/netcoreapp3.1/publish/ ThermoTracker/Station/
WORKDIR ThermoTracker/Station
ENTRYPOINT ["dotnet", "ThermoTracker.Station.dll"]
