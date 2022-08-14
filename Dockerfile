FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 433

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DBAccess.csproj", "."]
RUN dotnet restore "DBAccess.csproj"
COPY . .
RUN dotnet build "DBAccess.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DBAccess.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DBAccess.dll"]
