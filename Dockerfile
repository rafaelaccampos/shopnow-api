FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /ShopNow

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish "./ShopNow/ShopNow.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set environment
ENV ASPNETCORE_ENVIRONMENT=Development

# Set the port that application will run
EXPOSE 80

WORKDIR /ShopNow

COPY --from=build-env /ShopNow/out .

ENTRYPOINT ["dotnet", "ShopNow.dll"]