#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Identity/NetSchool.Identity/NetSchool.Identity.csproj", "Systems/Identity/NetSchool.Identity/"]
COPY ["Data/NetSchool.Context/NetSchool.Context.csproj", "Data/NetSchool.Context/"]
COPY ["Services/NetSchool.Services.Settings/NetSchool.Services.Settings.csproj", "Services/NetSchool.Services.Settings/"]
COPY ["Shared/NetSchool.Common/NetSchool.Common.csproj", "Shared/NetSchool.Common/"]
COPY ["Data/NetSchool.Context.Entities/NetSchool.Context.Entities.csproj", "Data/NetSchool.Context.Entities/"]
RUN dotnet restore "./Systems/Identity/NetSchool.Identity/./NetSchool.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/NetSchool.Identity"
RUN dotnet build "./NetSchool.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NetSchool.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetSchool.Identity.dll"]