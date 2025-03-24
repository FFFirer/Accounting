# Introduction

- 使用 ASP.NET Core Blazor 开发组件页面及接口
- 使用 solidjs 开发单页应用
- 使用 tailwindcss + daisyui 提供样式及主题
- 使用 YARP 开发时反向代理
- 使用 PostgreSQL 作为数据库

## 设计

- 以ASP.NET Core Blazor提供服务端能力，提供身份认证及授权，嵌入由solidjs编写的单页应用（支持路由）。
- 开发时使用`Vite`的DevServer响应热更新。
- 最终发布时，可以将生成的静态资源放在`wwwroot`下或使用CDN部署。
- 使用先进的前端生态。

# Development

## Restore packages

```bash
# 还原.net项目依赖
dotnet restore

# 安装npm包
pnpm install
```

## Environment requirements

**VS Code**

安装推荐的VSCode插件: 
  1. ms-dotnettools.csharp
  2. ms-dotnettools.csdevkit
  3. bradlc.vscode-tailwindcss
  4. hediet.vscode-drawio

## Run

使用命令行启动

```bash

# solidjs项目
pnpm run dev

# 运行时反向代理
dotnet run --project ./DevProxy/DevProxy.csproj -lp http 

# 运行Web项目
dotnet watch run --project ./Accounting.Web/Account.Web.csproj -lp http-headless
```

## Debug

**frontend**

使用`VSCode`调试配置文件

**Accounting.Web**

可以使用`VSCode`或`Visual Studio`调试
