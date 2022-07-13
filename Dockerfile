#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt update \
    && apt install -y libgdiplus libxkbcommon-x11-0 libc6 libc6-dev libgtk2.0-0 libnss3 libatk-bridge2.0-0 libx11-xcb1 libxcb-dri3-0 libdrm-common libgbm1 libasound2 libxrender1 libfontconfig1 libxshmfence1
# update write permissions
RUN chmod 777 .

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
#COPY ["Notipet.Web/Notipet.Web.csproj", "Notipet.Web/"]
RUN dotnet restore "Notipet.Web/Notipet.Web.csproj"
COPY . .
WORKDIR "/src/Notipet.Web"
RUN dotnet build "Notipet.Web.csproj" -c Release -o /app/build 

FROM build AS publish
WORKDIR "/src/Notipet.Web"
RUN dotnet publish "Notipet.Web.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#CMD ["dotnet", "Notipet.Web.dll"]
# CMD ASPNETCORE_URLS=http://*:$PORT dotnet Notipet.Web.dll
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Notipet.Web.dll
