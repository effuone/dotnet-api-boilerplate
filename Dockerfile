FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["BikeStores.Domain/BikeStores.Domain.csproj", "BikeStores.Domain/"]
COPY ["BikeStores.Application/BikeStores.Application.csproj", "BikeStores.Application/"]
COPY ["BikeStores.Api/BikeStores.Api.csproj", "BikeStores.Api/"]
RUN dotnet restore "BikeStores.Api/BikeStores.Api.csproj"
COPY . .
WORKDIR "/src/BikeStores.Api"
RUN dotnet build "BikeStores.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BikeStores.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BikeStores.Api.dll"]