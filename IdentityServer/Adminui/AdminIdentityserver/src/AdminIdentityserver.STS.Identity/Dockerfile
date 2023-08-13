FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/AdminIdentityserver.STS.Identity/AdminIdentityserver.STS.Identity.csproj", "src/AdminIdentityserver.STS.Identity/"]
COPY ["src/AdminIdentityserver.Admin.EntityFramework.Shared/AdminIdentityserver.Admin.EntityFramework.Shared.csproj", "src/AdminIdentityserver.Admin.EntityFramework.Shared/"]
COPY ["src/AdminIdentityserver.Shared/AdminIdentityserver.Shared.csproj", "src/AdminIdentityserver.Shared/"]
RUN dotnet restore "src/AdminIdentityserver.STS.Identity/AdminIdentityserver.STS.Identity.csproj"
COPY . .
WORKDIR "/src/src/AdminIdentityserver.STS.Identity"
RUN dotnet build "AdminIdentityserver.STS.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminIdentityserver.STS.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "AdminIdentityserver.STS.Identity.dll"]