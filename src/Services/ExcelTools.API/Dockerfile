#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/ExcelTools.API/ExcelTools.API.csproj", "Services/ExcelTools.API/"]
COPY ["Services/ExcelToolsApi.Domain/ExcelToolsApi.Domain.csproj", "Services/ExcelToolsApi.Domain/"]
COPY ["Services/ExcelToolsApi.Infraestructure/ExcelToolsApi.Infraestructure.csproj", "Services/ExcelToolsApi.Infraestructure/"]
COPY ["Services/ExcelToolsApi.Excel.Service/ExcelToolsApi.Excel.Service.csproj", "Services/ExcelToolsApi.Excel.Service/"]
COPY ["Services/ExcelToolsApi.Persistence/ExcelToolsApi.Persistence.csproj", "Services/ExcelToolsApi.Persistence/"]
COPY ["Services/ExcelToolsApi.JWT.Service/ExcelToolsApi.JWT.Service.csproj", "Services/ExcelToolsApi.JWT.Service/"]
RUN dotnet restore "./Services/ExcelTools.API/ExcelTools.API.csproj"
COPY . .
WORKDIR "/src/Services/ExcelTools.API"
RUN dotnet build "./ExcelTools.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ExcelTools.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExcelTools.API.dll"]