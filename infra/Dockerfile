FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Presentation/NightTasker.Passport.Presentation.WebApi/NightTasker.Passport.Presentation.WebApi.csproj", "Presentation/NightTasker.Passport.Presentation.WebApi/"]
COPY ["Infrastructure/NightTasker.Passport.Infrastructure.Identity/NightTasker.Passport.Infrastructure.Identity.csproj", "Infrastructure/NightTasker.Passport.Infrastructure.Identity/"]
COPY ["Core/NightTasker.Passport.Core.Application/NightTasker.Passport.Core.Application.csproj", "Core/NightTasker.Passport.Core.Application/"]
COPY ["Core/NightTasker.Passport.Core.Domain/NightTasker.Passport.Core.Domain.csproj", "Core/NightTasker.Passport.Core.Domain/"]
COPY ["Infrastructure/NightTasker.Passport.Infrastructure.Persistence/NightTasker.Passport.Infrastructure.Persistence.csproj", "Infrastructure/NightTasker.Passport.Infrastructure.Persistence/"]
RUN dotnet restore "Presentation/NightTasker.Passport.Presentation.WebApi/NightTasker.Passport.Presentation.WebApi.csproj"
COPY . .
WORKDIR "/src/Presentation/NightTasker.Passport.Presentation.WebApi"
RUN dotnet build "NightTasker.Passport.Presentation.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NightTasker.Passport.Presentation.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NightTasker.Passport.Presentation.WebApi.dll"]
