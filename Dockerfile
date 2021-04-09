FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY /home/vsts/work/1/drop/s/* .
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "e-paper-api.dll"]
