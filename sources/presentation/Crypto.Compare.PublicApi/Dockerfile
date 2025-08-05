FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine-amd64 AS build

ARG GIT_USERNAME=test
ARG GIT_PACKAGES_TOKEN=token

WORKDIR /app
COPY /sources ./sources
COPY /i18n ./i18n
COPY ./NuGet.Config ./

WORKDIR /app/sources/presentation/Crypto.Compare.PublicApi
RUN dotnet nuget update source gitCrispmind --username $GIT_USERNAME --password $GIT_PACKAGES_TOKEN --store-password-in-clear-text
RUN dotnet restore ./Crypto.Compare.PublicApi.csproj
RUN dotnet publish ./Crypto.Compare.PublicApi.csproj --no-restore -c Release -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine-amd64 AS base

RUN apk add --upgrade --no-cache \
    icu-libs \
    busybox

ENV \
DOTNET_RUNNING_IN_CONTAINER=true \
DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /publish
COPY --from=build /publish .

ENTRYPOINT ["dotnet", "Crypto.Compare.PublicApi.dll"]
