FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["authenticationApi/authenticationApi.csproj", "authenticationApi/"]
COPY ["PontoFacilSharedData/PontoFacilSharedData.csproj", "PontoFacilSharedData/"]
COPY ["PontoFacilWebService/PontoFacilWebService.csproj", "PontoFacilWebService/"]
RUN dotnet restore

COPY AuthenticationApi/. ./AuthenticationApi/
COPY PontoFacilSharedData/. ./PontoFacilSharedData/
COPY PontoFacilWebService/. ./PontoFacilWebService/

WORKDIR "/src/authenticationApi"
RUN dotnet publish "authenticationApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "authenticationApi.dll"]


