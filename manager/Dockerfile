FROM mcr.microsoft.com/dotnet/sdk AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore
COPY ./ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "manager.dll"]