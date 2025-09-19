FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine-amd64 AS build

WORKDIR /app
COPY /sources ./sources
COPY ./NuGet.Config ./

WORKDIR /app/sources/presentation/Crypto.Compare.PublicApi
RUN dotnet restore ./Crypto.Compare.PublicApi.csproj
RUN dotnet publish ./Crypto.Compare.PublicApi.csproj --no-restore -c Release -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine-amd64 AS base

ENV \
DOTNET_RUNNING_IN_CONTAINER=true \
DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /publish
COPY --from=build /publish .

ENTRYPOINT ["dotnet", "Crypto.Compare.PublicApi.dll"]
