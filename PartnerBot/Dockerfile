FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["PartnerBot/PartnerBot.csproj", "PartnerBot/"]
RUN dotnet restore "PartnerBot/PartnerBot.csproj"
COPY . .
WORKDIR "/src/PartnerBot"
RUN dotnet build "PartnerBot.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PartnerBot.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PartnerBot.dll"]