#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Systems/Worker/NetSchool.Worker/NetSchool.Worker.csproj", "Systems/Worker/NetSchool.Worker/"]
COPY ["Data/NetSchool.Context/NetSchool.Context.csproj", "Data/NetSchool.Context/"]
COPY ["Services/NetSchool.Services.Settings/NetSchool.Services.Settings.csproj", "Services/NetSchool.Services.Settings/"]
COPY ["Shared/NetSchool.Common/NetSchool.Common.csproj", "Shared/NetSchool.Common/"]
COPY ["Data/NetSchool.Context.Entities/NetSchool.Context.Entities.csproj", "Data/NetSchool.Context.Entities/"]
COPY ["Services/NetSchool.Services.Actions/NetSchool.Services.Actions.csproj", "Services/NetSchool.Services.Actions/"]
COPY ["Services/NetSchool.Services.RabbitMq/NetSchool.Services.RabbitMq.csproj", "Services/NetSchool.Services.RabbitMq/"]
COPY ["Services/NetSchool.Services.Logger/NetSchool.Services.Logger.csproj", "Services/NetSchool.Services.Logger/"]
RUN dotnet restore "./Systems/Worker/NetSchool.Worker/./NetSchool.Worker.csproj"
COPY . .
WORKDIR "/src/Systems/Worker/NetSchool.Worker"
RUN dotnet build "./NetSchool.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NetSchool.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetSchool.Worker.dll"]