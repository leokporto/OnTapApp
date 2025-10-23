FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OnTapApp.Web/OnTapApp.Web.csproj", "OnTapApp.Web/"]
COPY ["OnTapApp.Core/OnTapApp.Core.csproj", "OnTapApp.Core/"]
COPY ["OnTapApp.Web.Client/OnTapApp.Web.Client.csproj", "OnTapApp.Web.Client/"]
RUN dotnet restore "OnTapApp.Web/OnTapApp.Web.csproj"
COPY . .
WORKDIR "/src/OnTapApp.Web"


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnTapApp.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnTapApp.Web.dll"]
