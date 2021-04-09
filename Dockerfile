FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY . .
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "e-paper-api.dll"]
