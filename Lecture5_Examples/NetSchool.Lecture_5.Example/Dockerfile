#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NetSchool.Lecture_5.Example/NetSchool.Lecture_5.Example.csproj", "NetSchool.Lecture_5.Example/"]
RUN dotnet restore "NetSchool.Lecture_5.Example/NetSchool.Lecture_5.Example.csproj"
COPY . .
WORKDIR "/src/NetSchool.Lecture_5.Example"
RUN dotnet build "NetSchool.Lecture_5.Example.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetSchool.Lecture_5.Example.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetSchool.Lecture_5.Example.dll"]