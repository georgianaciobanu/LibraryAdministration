#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SimbioMed.Migrator/SimbioMed.Migrator.csproj", "src/SimbioMed.Migrator/"]
COPY ["src/SimbioMed.EntityFrameworkCore/SimbioMed.EntityFrameworkCore.csproj", "src/SimbioMed.EntityFrameworkCore/"]
COPY ["src/SimbioMed.Core/SimbioMed.Core.csproj", "src/SimbioMed.Core/"]
COPY ["src/SimbioMed.Core.Shared/SimbioMed.Core.Shared.csproj", "src/SimbioMed.Core.Shared/"]
RUN dotnet restore "src/SimbioMed.Migrator/SimbioMed.Migrator.csproj"
COPY . .
WORKDIR "/src/src/SimbioMed.Migrator"
RUN dotnet build "SimbioMed.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimbioMed.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimbioMed.Migrator.dll", "-s"]
