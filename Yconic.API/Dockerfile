FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
RUN mkdir -p wwwroot/uploads && chown -R app:app wwwroot/uploads
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Yconic.API/Yconic.API.csproj", "Yconic.API/"]
RUN dotnet restore "Yconic.API/Yconic.API.csproj"
COPY . .
WORKDIR "/src/Yconic.API"
RUN dotnet build "Yconic.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Yconic.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER root
RUN mkdir -p wwwroot/uploads && chown -R app:app wwwroot/uploads
USER app
ENTRYPOINT ["dotnet", "Yconic.API.dll"]
