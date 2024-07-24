#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

from mcr.microsoft.com/dotnet/aspnet:8.0 as base
user app
workdir /app
expose 8080
expose 8081
##
from mcr.microsoft.com/dotnet/sdk:8.0 as build
arg build_configuration=release
workdir /src
copy ["anorg.csproj", "."]
run dotnet restore "./anorg.csproj"
copy . .
workdir "/src/."
run dotnet build "./anorg.csproj" -c $build_configuration -o /app/build
##
from build as publish
arg build_configuration=release
run dotnet publish "./anorg.csproj" -c $build_configuration -o /app/publish /p:useapphost=false
##
from base as final
workdir /app
copy --from=publish /app/publish .
entrypoint ["dotnet", "anorg.dll"]
#