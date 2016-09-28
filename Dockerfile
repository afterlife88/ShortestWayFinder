FROM microsoft/dotnet:latest

# Set env variables
ENV ASPNETCORE_URLS http://*:5000

COPY /src/ShortestWayFinder.Web /app/src/ShortestWayFinder.Web
COPY /src/ShortestWayFinder.Domain /app/src/ShortestWayFinder.Domain

# Restore domain
WORKDIR /app/src/ShortestWayFinder.Domain
RUN ["dotnet", "restore"]

WORKDIR /app/src/ShortestWayFinder.Web
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
 
# Open port
EXPOSE 5000/tcp
 
ENTRYPOINT ["dotnet", "run"]