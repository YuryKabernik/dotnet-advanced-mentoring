#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY ./src/CatalogService/ .
RUN dotnet build WebApi/CatalogService.WebApi.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish WebApi/CatalogService.WebApi.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi/CatalogService.WebApi.dll"]