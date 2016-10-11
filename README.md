# Shortest Way Finder
### It-Challenges 2016 fall, back-end qualification round

## Demo
> - http://challenge-task.info


## Run in docker
### Pull image of application from docker hub and run on 8080 port:
> - **`docker run -p 8080:5000 itchallenges/shortest-way-finder`**
> - Run localhost:8080 in the browser, if the port is busy, change `8080` in `docker run` command to any available port and run again.

### Alternative way - build from sources and run:
> - `docker build -t app .`
> - `docker run -p 8080:5000 -t app`

## Technologies used:
**Backend:** ASP.NET Core, Entity Framework Core, Automapper, MS SQL, Swagger (Auto-generated documentation for API), XUnit, Moq.

**Frontend:** AngularJS, Bootstrap, JQuery, Alertify.js, SigmaJS(graph drawing on the view)

## Project structure
- **ShortestWayFinder.Web** - REST API, project contains controllers, services etc.
- **ShortestWayFinder.Web/wwwroot** - client with AngularJS
- **ShortestWayFinder.Domain** - the project that contains working with data (contracts, repositories, ORM Models) and main Dijkstra algorithm for finding shortest path on the graph.
- **ShortestWayFinder.Tests** - Unit tests on an algorithm and for testing controllers logic.

## Internal dependencies
- **ShortestWayFinder.Web** -> ShortestWayFinder.Domain
- **ShortestWayFinder.Tests** -> ShortestWayFinder.Domain, ShortestWayFinder.Web

## Task
Barcelona is a famous part of Europe with a lot of tourist attractions, hotels, restaurants. You
can visit many beautiful locations. But of course, moving from point A to point B takes time.
When your time is limited, you may want to optimize your tour and find the shortest path
between two points on the map.

Prepare a web application, where users will be able to add, delete and edit connections on
the map, each connection will be described by:

1. name of the first point
2. name of the second point
3. number of minutes/hours needed to travel between these points.
4. ask to find a shortest path between two points on the map, for example, by selecting
them from dropdown menus; as the result, total number of minutes in the shortest
path between them should be displayed.

As you may notice, one point may be connected to many other points on the map. As a data
storage you may use any SQL database or text files, only databases supporting graph
structures are forbidden.