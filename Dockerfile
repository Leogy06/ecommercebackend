# Dockerfile (Standard Multi-Stage Build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /EcommerceBackend

# Copy and restore project files
COPY ["EcommerceBackend/EcommerceBackend.csproj", "EcommerceBackend/"]
RUN dotnet restore "MyApiProject/MyApiProject.csproj"

# Copy the rest of the application code
COPY . .
WORKDIR "/EcommerceBackend"
RUN dotnet publish "EcommerceBackend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Ensure it listens on the correct port (8080 is the new .NET default)
ENV ASPNETCORE_HTTP_PORTS=8080 
EXPOSE 8080

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcommerceBackend.dll"]