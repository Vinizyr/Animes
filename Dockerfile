# Etapa base (usada para rodar em produ��o)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa de build (compila��o)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar os arquivos de projeto necess�rios
COPY ["AnimeCatalogo.Api/AnimeCatalogo.Api.csproj", "AnimeCatalogo.Api/"]
COPY ["AnimeCatalogo.Application/AnimeCatalogo.Application.csproj", "AnimeCatalogo.Application/"]
COPY ["AnimeCatalogo.Infrastructure/AnimeCatalogo.Infrastructure.csproj", "AnimeCatalogo.Infrastructure/"]
COPY ["AnimeCatalogo.Domain/AnimeCatalogo.Domain.csproj", "AnimeCatalogo.Domain/"]
COPY ["tests/Domain/AnimeCatalogo.Domain.Tests/AnimeCatalogo.Domain.Tests.csproj", "tests/Domain/AnimeCatalogo.Domain.Tests/"]

# Restaurar depend�ncias do projeto
RUN dotnet restore "AnimeCatalogo.Api/AnimeCatalogo.Api.csproj"

# Copiar o c�digo fonte restante
COPY . .

# Compilar a aplica��o
WORKDIR "/src/AnimeCatalogo.Api"
RUN dotnet build "AnimeCatalogo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publica��o (preparar os arquivos para produ��o)
FROM build AS publish
RUN dotnet publish "AnimeCatalogo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final (rodando a aplica��o no cont�iner)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnimeCatalogo.Api.dll"]
