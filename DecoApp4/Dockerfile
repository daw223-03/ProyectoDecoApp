#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
RUN apt-get update && apt-get install -y wget fontconfig libfontconfig1 libjpeg62-turbo libxrender1 xfonts-75dpi xfonts-base libxext6 git
RUN wget https://github.com/wkhtmltopdf/packaging/releases/download/0.12.6.1-2/wkhtmltox_0.12.6.1-2.bullseye_amd64.deb
RUN dpkg -i wkhtmltox_0.12.6.1-2.bullseye_amd64.deb
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DecoApp4/DecoApp4.csproj", "DecoApp4/"]
RUN dotnet restore "DecoApp4/DecoApp4.csproj"
COPY . .
WORKDIR "/src/DecoApp4"
RUN dotnet build "DecoApp4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DecoApp4.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DecoApp4.dll"]
