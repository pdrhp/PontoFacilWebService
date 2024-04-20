FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["authenticationApi/authenticationApi.csproj", "authenticationApi/"]
COPY ["PontoFacilSharedData/PontoFacilSharedData.csproj", "PontoFacilSharedData/"]
COPY ["PontoFacilWebService/PontoFacilWebService.csproj", "PontoFacilWebService/"]

COPY authenticationApi/. ./authenticationApi/
COPY PontoFacilSharedData/. ./PontoFacilSharedData/
COPY PontoFacilWebService/. ./PontoFacilWebService/

RUN dotnet restore "authenticationApi/authenticationApi.csproj"

WORKDIR "/src/authenticationApi"
RUN dotnet publish "authenticationApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
USER $APP_UID
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "authenticationApi.dll"]


