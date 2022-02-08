FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gianni.Giudice.FeatureMatching.Console/Gianni.Giudice.FeatureMatching.Console.csproj", "Gianni.Giudice.FeatureMatching.Console/"]
RUN dotnet restore "Gianni.Giudice.FeatureMatching.Console/Gianni.Giudice.FeatureMatching.Console.csproj"
COPY . .
WORKDIR "/src/Gianni.Giudice.FeatureMatching.Console"
RUN dotnet build "Gianni.Giudice.FeatureMatching.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gianni.Giudice.FeatureMatching.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gianni.Giudice.FeatureMatching.Console.dll"]
