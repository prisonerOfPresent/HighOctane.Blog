FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY *.sln .
COPY HighOctane.Blog/*.csproj HighOctane.Blog/
RUN dotnet restore
COPY . .


FROM build-env AS publish
WORKDIR /app/HighOctane.Blog
RUN dotnet publish -c Release -o /app/HighOctane.Blog

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/HighOctane.Blog .
##ENTRYPOINT ["dotnet","HighOctane.Blog.dll"]

CMD ASPNETCORE_URLS=http://*:$PORT dotnet HighOctane.Blog.dll
