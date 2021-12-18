#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
# EXPOSE 80 removed as this is set in docker-compose

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["BethanysShop/BethanysShop.csproj", "BethanysShop/"]
RUN dotnet restore "BethanysShop/BethanysShop.csproj"
COPY . .
WORKDIR "/src/BethanysShop"
RUN dotnet build "BethanysShop.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BethanysShop.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BethanysShop.dll"]
