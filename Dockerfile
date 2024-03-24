#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./src/Services/ExcelTools.API/ExcelTools.API.csproj", "ExcelTools.API/"]
COPY ["./src/Services/ExcelToolsApi.Domain/ExcelToolsApi.Domain.csproj", "ExcelToolsApi.Domain/"]
COPY ["./src/Services/ExcelToolsApi.Infraestructure/ExcelToolsApi.Infraestructure.csproj", "ExcelToolsApi.Infraestructure/"]
COPY ["./src/Services/ExcelToolsApi.Excel.Service/ExcelToolsApi.Excel.Service.csproj", "ExcelToolsApi.Excel.Service/"]
COPY ["./src/Services/ExcelToolsApi.JWT.Service/ExcelToolsApi.JWT.Service.csproj", "ExcelToolsApi.JWT.Service/"]
COPY ["./src/Services/ExcelToolsApi.Persistence/ExcelToolsApi.Persistence.csproj", "ExcelToolsApi.Persistence/"]
RUN dotnet restore "ExcelTools.API/ExcelTools.API.csproj"
COPY . .
WORKDIR "ExcelTools.API"
RUN dotnet build "ExcelTools.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExcelTools.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExcelTools.API.dll"]