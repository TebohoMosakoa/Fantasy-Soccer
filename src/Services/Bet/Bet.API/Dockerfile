#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/Bet/Bet.API/Bet.API.csproj", "src/Services/Bet/Bet.API/"]
COPY ["src/Services/Bet/Bet.Infrastructure/Bet.Infrastructure.csproj", "src/Services/Bet/Bet.Infrastructure/"]
COPY ["src/Services/Bet/Bet.Application/Bet.Application.csproj", "src/Services/Bet/Bet.Application/"]
COPY ["src/Services/Bet/Bet.Domain/Bet.Domain.csproj", "src/Services/Bet/Bet.Domain/"]
RUN dotnet restore "src/Services/Bet/Bet.API/Bet.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Bet/Bet.API"
RUN dotnet build "Bet.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bet.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bet.API.dll"]
