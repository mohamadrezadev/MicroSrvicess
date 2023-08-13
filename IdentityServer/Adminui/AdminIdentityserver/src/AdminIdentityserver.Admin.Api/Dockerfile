FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AdminIdentityserver.Admin.Api/AdminIdentityserver.Admin.Api.csproj", "src/AdminIdentityserver.Admin.Api/"]
COPY ["src/AdminIdentityserver.Admin.EntityFramework.Shared/AdminIdentityserver.Admin.EntityFramework.Shared.csproj", "src/AdminIdentityserver.Admin.EntityFramework.Shared/"]
COPY ["src/AdminIdentityserver.Admin.EntityFramework.SqlServer/AdminIdentityserver.Admin.EntityFramework.SqlServer.csproj", "src/AdminIdentityserver.Admin.EntityFramework.SqlServer/"]
COPY ["src/AdminIdentityserver.Shared/AdminIdentityserver.Shared.csproj", "src/AdminIdentityserver.Shared/"]
COPY ["src/AdminIdentityserver.Admin.EntityFramework.PostgreSQL/AdminIdentityserver.Admin.EntityFramework.PostgreSQL.csproj", "src/AdminIdentityserver.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/AdminIdentityserver.Admin.EntityFramework.MySql/AdminIdentityserver.Admin.EntityFramework.MySql.csproj", "src/AdminIdentityserver.Admin.EntityFramework.MySql/"]
RUN dotnet restore "src/AdminIdentityserver.Admin.Api/AdminIdentityserver.Admin.Api.csproj"
COPY . .
WORKDIR "/src/src/AdminIdentityserver.Admin.Api"
RUN dotnet build "AdminIdentityserver.Admin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminIdentityserver.Admin.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "AdminIdentityserver.Admin.Api.dll"]