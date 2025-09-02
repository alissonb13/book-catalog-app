FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5296
EXPOSE 5296

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BookCatalog.Api/BookCatalog.Api.csproj", "src/BookCatalog.Api/"]
RUN dotnet restore "src/BookCatalog.Api/BookCatalog.Api.csproj"
COPY . .
WORKDIR "/src/src/BookCatalog.Api"
RUN dotnet build "./BookCatalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BookCatalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "BookCatalog.Api.dll"]