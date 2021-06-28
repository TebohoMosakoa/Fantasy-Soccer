#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/League/League.GRPC/League.GRPC.csproj", "src/Services/League/League.GRPC/"]
RUN dotnet restore "src/Services/League/League.GRPC/League.GRPC.csproj"
COPY . .
WORKDIR "/src/src/Services/League/League.GRPC"
RUN dotnet build "League.GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "League.GRPC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "League.GRPC.dll"]
