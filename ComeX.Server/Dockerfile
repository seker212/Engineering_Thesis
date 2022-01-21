#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ComeX.Server/ComeX.Server.csproj", "ComeX.Server/"]
COPY ["ComeX.Lib.Auth/ComeX.Lib.Auth.csproj", "ComeX.Lib.Auth/"]
COPY ["ComeX.Lib.Common/ComeX.Lib.Common.csproj", "ComeX.Lib.Common/"]
COPY ["Comex.Server.AdminCli/Comex.Server.AdminCli.csproj", "Comex.Server.AdminCli/"]
RUN dotnet restore "ComeX.Server/ComeX.Server.csproj"
COPY . .
WORKDIR "/src/ComeX.Server"
RUN dotnet build "ComeX.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ComeX.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ComeX.Server.dll"]