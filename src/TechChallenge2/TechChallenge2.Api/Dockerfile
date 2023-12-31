#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TechChallenge2.Api/TechChallenge2.Api.csproj", "TechChallenge2.Api/"]
RUN dotnet restore "TechChallenge2.Api/TechChallenge2.Api.csproj"
COPY . .
WORKDIR "/src/TechChallenge2.Api"
RUN dotnet build "TechChallenge2.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechChallenge2.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallenge2.Api.dll"]