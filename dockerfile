# .NET Core SDK
FROM mcr.microsoft.com/dotnet/core/sdk:3.1.202-alpine AS dotnetcore-sdk

WORKDIR .

# Copy All Files
COPY . .

# .NET Core Restore
RUN dotnet restore ./RefactorThisV2/RefactorThisV2.csproj

# .NET Core Build and Publish
FROM dotnetcore-sdk AS dotnetcore-build
RUN dotnet publish ./RefactorThisV2/RefactorThisV2.csproj -c Release -o /publish

# ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.4-alpine AS aspnetcore-runtime
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY --from=dotnetcore-build /publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "RefactorThisV2.dll"]