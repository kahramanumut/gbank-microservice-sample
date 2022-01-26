FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS="http://*:5001"

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY GBank.IdentityServer.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release

FROM build AS publish
RUN dotnet publish -c Release -o /publish

FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "GBank.IdentityServer.dll","--environment=Development"]