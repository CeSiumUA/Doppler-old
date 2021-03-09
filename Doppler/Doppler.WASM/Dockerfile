#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Doppler.WASM/Doppler.WASM.csproj", "Doppler.WASM/"]
RUN dotnet restore "Doppler.WASM/Doppler.WASM.csproj"
COPY . .
WORKDIR "/src/Doppler.WASM"
RUN dotnet build "Doppler.WASM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Doppler.WASM.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Doppler.WASM.dll"]