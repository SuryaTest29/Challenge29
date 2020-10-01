FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /Pokemon

COPY ["Pokemon.Clients/Pokemon.Clients.csproj", "Pokemon.Clients/"]
COPY ["Pokemon.Data/Pokemon.Data.csproj", "Pokemon.Data/"]
COPY ["Pokemon.Models/Pokemon.Models.csproj", "Pokemon.Models/"]
COPY ["Pokemon.Search/Pokemon.Search.csproj", "Pokemon.Search/"]
COPY ["Pokemon.Services/Pokemon.Services.csproj", "Pokemon.Services/"]
COPY ["Pokemon.Search.Test/Pokemon.Search.Test.csproj", "Pokemon.Search.Test/"]

#copy everything else and build
COPY . .
WORKDIR /Pokemon

RUN dotnet build "./Pokemon.Search/Pokemon.Search.csproj" -c Release -o /api
#RUN dotnet build "./Pokemon.Search.Test/Pokemon.Search.Test.csproj" -c Release -o /tests

FROM build AS publish
RUN dotnet publish -c Release "./Pokemon.Search/Pokemon.Search.csproj" -o /app/api
#RUN dotnet publish -c Release "./Pokemon.Search.Test/Pokemon.Search.Test.csproj" -o app/tests

FROM base AS final
WORKDIR /app
COPY --from=publish /app/api .
#COPY --from=publish /app/tests .
ENTRYPOINT ["dotnet", "Pokemon.Search.dll"]