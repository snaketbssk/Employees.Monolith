#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ARG branch
ENV ASPNETCORE_BRANCH $branch

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Employees.Monolith.Api/Employees.Monolith.Api.csproj", "Employees.Monolith.Api/"]
COPY ["Employees.Monolith.Connections/Employees.Monolith.Connections.csproj", "Employees.Monolith.Connections/"]
COPY ["Employees.Monolith.LogicLayer/Employees.Monolith.LogicLayer.csproj", "Employees.Monolith.LogicLayer/"]
COPY ["Employees.Monolith.DataLayer/Employees.Monolith.DataLayer.csproj", "Employees.Monolith.DataLayer/"]
COPY ["Employees.Monolith.Configurations/Employees.Monolith.Configurations.csproj", "Employees.Monolith.Configurations/"]
RUN dotnet restore "Employees.Monolith.Api/Employees.Monolith.Api.csproj"
COPY . .
WORKDIR "/src/Employees.Monolith.Api"
RUN dotnet build "Employees.Monolith.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Employees.Monolith.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Employees.Monolith.Api.dll"]