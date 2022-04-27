#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/SimbioMed.Web.Public/SimbioMed.Web.Public.csproj", "src/SimbioMed.Web.Public/"]
COPY ["src/SimbioMed.Web.Core/SimbioMed.Web.Core.csproj", "src/SimbioMed.Web.Core/"]
COPY ["src/SimbioMed.Application/SimbioMed.Application.csproj", "src/SimbioMed.Application/"]
COPY ["src/SimbioMed.Application.Shared/SimbioMed.Application.Shared.csproj", "src/SimbioMed.Application.Shared/"]
COPY ["src/SimbioMed.Core.Shared/SimbioMed.Core.Shared.csproj", "src/SimbioMed.Core.Shared/"]
COPY ["src/SimbioMed.Core/SimbioMed.Core.csproj", "src/SimbioMed.Core/"]
COPY ["src/SimbioMed.EntityFrameworkCore/SimbioMed.EntityFrameworkCore.csproj", "src/SimbioMed.EntityFrameworkCore/"]
COPY ["src/SimbioMed.GraphQL/SimbioMed.GraphQL.csproj", "src/SimbioMed.GraphQL/"]
RUN dotnet restore "src/SimbioMed.Web.Public/SimbioMed.Web.Public.csproj"
COPY . .
WORKDIR "/src/src/SimbioMed.Web.Public"
RUN dotnet build "SimbioMed.Web.Public.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimbioMed.Web.Public.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimbioMed.Web.Public.dll"]
