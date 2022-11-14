#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Banhcafe.Microservices.CableColor.Api/Banhcafe.Microservices.CableColor.Api.csproj", "Banhcafe.Microservices.CableColor.Api/"]
RUN dotnet restore "Banhcafe.Microservices.CableColor.Api/Banhcafe.Microservices.CableColor.Api.csproj"
COPY . .
WORKDIR "/src/Banhcafe.Microservices.CableColor.Api"
RUN dotnet build "Banhcafe.Microservices.CableColor.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Banhcafe.Microservices.CableColor.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Banhcafe.Microservices.CableColor.Api.dll"]