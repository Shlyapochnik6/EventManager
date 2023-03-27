FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EventManager.WebAPI/EventManager.WebAPI.csproj", "EventManager.WebAPI/"]
COPY ["EventManager.Persistence/EventManager.Persistence.csproj", "EventManager.Persistence/"]
COPY ["EventManager.Application/EventManager.Application.csproj", "EventManager.Application/"]
COPY ["EventManager.Domain/EventManager.Domain.csproj", "EventManager.Domain/"]
COPY ["EventManager.Identity/EventManager.Identity.csproj", "EventManager.Identity/"]
RUN dotnet restore "EventManager.WebAPI/EventManager.WebAPI.csproj"
RUN dotnet restore "EventManager.Persistence/EventManager.Persistence.csproj"
RUN dotnet restore "EventManager.Application/EventManager.Application.csproj"
RUN dotnet restore "EventManager.Domain/EventManager.Domain.csproj"
RUN dotnet restore "EventManager.Identity/EventManager.Identity.csproj"
COPY . .
WORKDIR "/src/EventManager.WebAPI"
RUN dotnet build "EventManager.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventManager.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventManager.WebAPI.dll"]
