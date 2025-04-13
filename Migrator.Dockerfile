ARG DOTNET_VERSION=${DOTNET_VERSION:-10.0-preview-alpine}

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS dotnet-build

ARG BUILD_CONFIGURATION=${BUILD_CONFIGURATION:-Release}

COPY ["./Accounting.Abstractions/Accounting.Abstractions.csproj", "/src/Accounting.Abstractions/"]
COPY ["./Accounting.Core/Accounting.Core.csproj", "/src/Accounting.Core/"]
COPY ["./Accounting.Migrator/Accounting.Migrator.csproj", "/src/Accounting.Migrator/"]
COPY ["./Accounting.Quartz/Accounting.Quartz.csproj", "/src/Accounting.Quartz/"]
COPY ["./Accounting.Quartz.Migrations/Accounting.Quartz.Migrations.csproj", "/src/Accounting.Quartz.Migrations/"]
COPY ["./Accounting.Stores/Accounting.Stores.csproj", "/src/Accounting.Stores/"]
COPY ["./Directory.Build.props", "./Directory.Packages.props", "/src/"]

WORKDIR /src
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages dotnet restore "./Accounting.Migrator/Accounting.Migrator.csproj"

COPY ["./Accounting.Abstractions", "./Accounting.Abstractions"]
COPY ["./Accounting.Core", "./Accounting.Core"]
COPY ["./Accounting.Migrator", "./Accounting.Migrator"]
COPY ["./Accounting.Quartz", "./Accounting.Quartz"]
COPY ["./Accounting.Quartz.Migrations", "./Accounting.Quartz.Migrations"]
COPY ["./Accounting.Stores", "./Accounting.Stores"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages dotnet build "./Accounting.Migrator/Accounting.Migrator.csproj" -c $BUILD_CONFIGURATION -o /app/build 

FROM dotnet-build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Accounting.Migrator/Accounting.Migrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounting.Migrator.dll", "database", "update"]