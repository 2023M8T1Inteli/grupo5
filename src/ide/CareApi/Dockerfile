# Use a imagem do SDK .NET para construir o projeto
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copie o csproj e restaure as dependências
COPY src/CareApi/*.csproj ./src/CareApi/
RUN dotnet restore src/CareApi/CareApi.csproj

# Copie o resto dos arquivos e construa o projeto
COPY src/CareApi/ ./src/CareApi/
WORKDIR /app/src/CareApi
RUN dotnet publish -c Release -o out

# Gere a imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/src/CareApi/out .
ENTRYPOINT ["dotnet", "CareApi.dll"]

# Exponha a porta que o ASP.NET Core usa por padrão
EXPOSE 80
