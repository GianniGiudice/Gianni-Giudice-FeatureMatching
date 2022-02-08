FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gianni.Giudice.FeatureMatching.WebApi/Gianni.Giudice.FeatureMatching.WebApi.csproj", "Gianni.Giudice.FeatureMatching.WebApi/"]
RUN dotnet restore "Gianni.Giudice.FeatureMatching.WebApi/Gianni.Giudice.FeatureMatching.WebApi.csproj"
COPY . .
WORKDIR "/src/Gianni.Giudice.FeatureMatching.WebApi"
RUN dotnet build "Gianni.Giudice.FeatureMatching.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gianni.Giudice.FeatureMatching.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gianni.Giudice.FeatureMatching.WebApi.dll"]
