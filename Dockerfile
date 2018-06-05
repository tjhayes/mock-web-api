FROM microsoft/aspnetcore-build:2.0

WORKDIR /app
# copy everything from project folder into container
COPY ./ .

# package restore --
RUN dotnet restore \
&& dotnet build \
&& dotnet publish -o ./out

WORKDIR /app/out

ENTRYPOINT ["dotnet", "MockWebApi.dll"]