FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NightTaskerPassport.Presentation/NightTaskerPassport.Presentation.csproj", "NightTaskerPassport.Presentation/"]
RUN dotnet restore "NightTaskerPassport.Presentation/NightTaskerPassport.Presentation.csproj"
COPY . .
WORKDIR "/src/NightTaskerPassport.Presentation"
RUN dotnet build "NightTaskerPassport.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NightTaskerPassport.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NightTaskerPassport.Presentation.dll"]
