FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Presentation/NightTasker.Identity.Presentation.WebApi/NightTasker.Identity.Presentation.WebApi.csproj", "Presentation/NightTasker.Identity.Presentation.WebApi/"]
COPY ["Infrastructure/NightTasker.Identity.Infrastructure.Identity/NightTasker.Identity.Infrastructure.Identity.csproj", "Infrastructure/NightTasker.Identity.Infrastructure.Identity/"]
COPY ["Core/NightTasker.Identity.Core.Application/NightTasker.Identity.Core.Application.csproj", "Core/NightTasker.Identity.Core.Application/"]
COPY ["Core/NightTasker.Identity.Core.Domain/NightTasker.Identity.Core.Domain.csproj", "Core/NightTasker.Identity.Core.Domain/"]
COPY ["Infrastructure/NightTasker.Identity.Infrastructure.Persistence/NightTasker.Identity.Infrastructure.Persistence.csproj", "Infrastructure/NightTasker.Identity.Infrastructure.Persistence/"]
RUN dotnet restore "Presentation/NightTasker.Identity.Presentation.WebApi/NightTasker.Identity.Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/Presentation/NightTasker.Identity.Presentation.WebApi"
RUN dotnet build "NightTasker.Identity.Presentation.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NightTasker.Identity.Presentation.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NightTasker.Identity.Presentation.WebApi.dll"]
