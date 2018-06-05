FROM microsoft/aspnetcore-build:2.0 AS build

WORKDIR /app

COPY ./MockWebApi.csproj .
RUN dotnet restore
COPY ./ .

RUN dotnet build --no-restore \
&& dotnet publish -o ./out

FROM microsoft/aspnetcore:2.0 AS runtime
WORKDIR /app
COPY --from=build /app/out/ .

WORKDIR /app/out

ENTRYPOINT ["dotnet", "MockWebApi.dll"]