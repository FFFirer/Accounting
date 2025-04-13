ARG NODE_VERSION=22
ARG DOTNET_VERSION=10.0-preview-alpine

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM node:${NODE_VERSION}-alpine AS node-base

ARG NPM_REGISTRY=http://10.9.0.1:4873

ENV PNPM_HOME="/pnpm"
ENV PATH="$PNPM_HOME:$PATH"
RUN corepack enable

RUN npm config set registry ${NPM_REGISTRY}

FROM node-base AS node-build

COPY ["./package.json", "./pnpm-lock.yaml", "./tailwind.config.js", "./tsconfig.json", "./index.html", "./vite.config.ts", "/src/"]

WORKDIR /src
RUN --mount=type=cache,id=pnpm,target=/pnpm/store pnpm install --frozen-lockfile

COPY ["./Accounting.Web", "./Accounting.Web"]
COPY ["./Accounting.Web.Client", "./Accounting.Web.Client"]
COPY ["./frontend", "./frontend"]

RUN pnpm run build
RUN pnpm run css:build

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS dotnet-build
ARG BUILD_CONFIGURATION=${BUILD_CONFIGURATION:-Release}

COPY ["./Accounting.Abstractions/Accounting.Abstractions.csproj", "/src/Accounting.Abstractions/"]
COPY ["./Accounting.Core/Accounting.Core.csproj", "/src/Accounting.Core/"]
COPY ["./Accounting.Quartz/Accounting.Quartz.csproj", "/src/Accounting.Quartz/"]
COPY ["./Accounting.Stores/Accounting.Stores.csproj", "/src/Accounting.Stores/"]
COPY ["./Accounting.Web/Accounting.Web.csproj", "/src/Accounting.Web/"]
COPY ["./Accounting.Web.Client/Accounting.Web.Client.csproj", "/src/Accounting.Web.Client/"]
COPY ["./Directory.Build.props", "./Directory.Packages.props", "/src/"]

WORKDIR /src
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages dotnet restore "./Accounting.Web/Accounting.Web.csproj"

COPY ["./Accounting.Abstractions", "./Accounting.Abstractions"]
COPY ["./Accounting.Core", "./Accounting.Core"]
COPY ["./Accounting.Quartz", "./Accounting.Quartz"]
COPY ["./Accounting.Stores", "./Accounting.Stores"]
COPY ["./Accounting.Web", "./Accounting.Web"]
COPY ["./Accounting.Web.Client", "./Accounting.Web.Client"]

RUN rm -rf "/src/Accounting.Web/wwwroot/frontend/*"
COPY --from=node-build ["/src/dist/*", "./Accounting.Web/wwwroot/frontend/"]

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages dotnet build "./Accounting.Web/Accounting.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build 

FROM dotnet-build AS publish
RUN dotnet publish "./Accounting.Web/Accounting.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounting.Web.dll"]