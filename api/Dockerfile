FROM microsoft/aspnetcore-build as publisher

WORKDIR /publish

COPY DiffProject.csproj .

RUN dotnet restore

COPY . .

RUN dotnet publish --output ./out

FROM microsoft/aspnetcore as published

WORKDIR /dotnetapp
COPY --from=publisher /publish/out .
ENV ASPNETCORE_URLS "http://0.0.0.0:5000/"
ENTRYPOINT ["dotnet", "DiffProject.dll"]