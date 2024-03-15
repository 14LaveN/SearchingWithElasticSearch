FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SearchingWithElasticSearch.Api/SearchingWithElasticSearch.Api.csproj", "SearchingWithElasticSearch.Api/"]
RUN dotnet restore "SearchingWithElasticSearch.Api/SearchingWithElasticSearch.Api.csproj"
COPY . .
WORKDIR "/src/SearchingWithElasticSearch.Api"
RUN dotnet build "SearchingWithElasticSearch.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SearchingWithElasticSearch.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SearchingWithElasticSearch.Api.dll"]
