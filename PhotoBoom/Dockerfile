FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["PhotoBoom/PhotoBoom.csproj", "PhotoBoom/"]
RUN dotnet restore "PhotoBoom/PhotoBoom.csproj"
COPY . .
WORKDIR "/src/PhotoBoom"
RUN dotnet build "PhotoBoom.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PhotoBoom.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PhotoBoom.dll"]