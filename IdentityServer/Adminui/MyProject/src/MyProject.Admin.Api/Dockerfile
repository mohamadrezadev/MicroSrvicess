FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/MyProject.Admin.Api/MyProject.Admin.Api.csproj", "src/MyProject.Admin.Api/"]
COPY ["src/MyProject.Admin.EntityFramework.Shared/MyProject.Admin.EntityFramework.Shared.csproj", "src/MyProject.Admin.EntityFramework.Shared/"]
COPY ["src/MyProject.Admin.EntityFramework.SqlServer/MyProject.Admin.EntityFramework.SqlServer.csproj", "src/MyProject.Admin.EntityFramework.SqlServer/"]
COPY ["src/MyProject.Shared/MyProject.Shared.csproj", "src/MyProject.Shared/"]
COPY ["src/MyProject.Admin.EntityFramework.PostgreSQL/MyProject.Admin.EntityFramework.PostgreSQL.csproj", "src/MyProject.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/MyProject.Admin.EntityFramework.MySql/MyProject.Admin.EntityFramework.MySql.csproj", "src/MyProject.Admin.EntityFramework.MySql/"]
RUN dotnet restore "src/MyProject.Admin.Api/MyProject.Admin.Api.csproj"
COPY . .
WORKDIR "/src/src/MyProject.Admin.Api"
RUN dotnet build "MyProject.Admin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyProject.Admin.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "MyProject.Admin.Api.dll"]