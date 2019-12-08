FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TrygramParserAPI.csproj", "."]
RUN dotnet restore "TrygramParserAPI.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "TrygramParserAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrygramParserAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrygramParserAPI.dll"]