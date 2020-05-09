# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.sln .
COPY HighOctane.Blog/*.csproj HighOctane.Blog/
RUN dotnet restore
COPY . .

# testing
FROM build AS testing
WORKDIR /src/HighOctane.Blog
RUN dotnet build

# publish
FROM build AS publish
WORKDIR /src/HighOctane.Blog
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .
#ENTRYPOINT ["dotnet", "HighOctane.Blog.dll"]
# heroku uses the following
CMD ASPNETCORE_URLS=http://*:$PORT dotnet HighOctane.Blog.dll
