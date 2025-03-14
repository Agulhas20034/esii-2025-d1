FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["esii-2025-d1/esii-2025-d1.csproj", "esii-2025-d1/"]
RUN dotnet restore "esii-2025-d1/esii-2025-d1.csproj"
COPY . .
WORKDIR "/src/esii-2025-d1"
RUN dotnet build "esii-2025-d1.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "esii-2025-d1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "esii-2025-d1.dll"]
#oiiiiii