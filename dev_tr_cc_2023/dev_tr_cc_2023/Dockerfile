#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["dev_tr_cc_2023/dev_tr_cc_2023.csproj", "dev_tr_cc_2023/"]
RUN dotnet restore "dev_tr_cc_2023/dev_tr_cc_2023.csproj"
COPY . .
WORKDIR "/src/dev_tr_cc_2023"
RUN dotnet build "dev_tr_cc_2023.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dev_tr_cc_2023.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 1234/udp
ENTRYPOINT ["dotnet", "dev_tr_cc_2023.dll"]