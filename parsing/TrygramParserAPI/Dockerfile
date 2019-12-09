#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["parsing/TrygramParserAPI/TrygramParserAPI.csproj", "parsing/TrygramParserAPI/"]
COPY ["parsing/SengenceGenerator/SentenceGenerator.csproj", "parsing/SengenceGenerator/"]
COPY ["parsing/Shared/Shared.csproj", "parsing/Shared/"]
RUN dotnet restore "parsing/TrygramParserAPI/TrygramParserAPI.csproj"
COPY . .
WORKDIR "/src/parsing/TrygramParserAPI"
RUN dotnet build "TrygramParserAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrygramParserAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrygramParserAPI.dll"]