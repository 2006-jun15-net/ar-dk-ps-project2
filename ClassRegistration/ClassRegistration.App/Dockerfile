#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ClassRegistration.App/ClassRegistration.App.csproj", "ClassRegistration.App/"]
COPY ["ClassRegistration.Domain/ClassRegistration.Domain.csproj", "ClassRegistration.Domain/"]
COPY ["ClassRegistration.DataAccess/ClassRegistration.DataAccess.csproj", "ClassRegistration.DataAccess/"]
RUN dotnet restore "ClassRegistration.App/ClassRegistration.App.csproj"
COPY . .
WORKDIR "/src/ClassRegistration.App"
RUN dotnet build "ClassRegistration.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClassRegistration.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClassRegistration.App.dll"]