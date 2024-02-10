#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["Resource.API/Resource.API.csproj", "Resource.API/"]
COPY ["Resource.Business/Resource.Business.csproj", "Resource.Business/"]
COPY ["Resource.Repository/Resource.Repository.csproj", "Resource.Repository/"]
COPY ["Resource.Shared/Resource.Shared.csproj", "Resource.Shared/"]
RUN dotnet restore "./Resource.API/./Resource.API.csproj"
COPY . .
WORKDIR "/src/Resource.API"
RUN dotnet build "./Resource.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Resource.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Resource.API.dll"]