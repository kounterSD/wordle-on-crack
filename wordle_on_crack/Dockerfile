FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["wordle_on_crack/wordle_on_crack.csproj", "wordle_on_crack/"]
RUN dotnet restore "wordle_on_crack/wordle_on_crack.csproj"
COPY . .
WORKDIR "/src/wordle_on_crack"
RUN dotnet build "wordle_on_crack.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "wordle_on_crack.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wordle_on_crack.dll"]
