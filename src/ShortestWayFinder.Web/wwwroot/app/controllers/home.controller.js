(function (angular) {
  'use strict';

  angular.module('app').controller('HomeController', HomeController);

  HomeController.$inject = ['PathService', '$q', 'Alertify'];

  function HomeController(PathService, $q, Alertify) {

    var vm = this;
    var sigmaGraph;

    vm.points = [];
    vm.allPaths = [];
    vm.shortestPathData = {};
    vm.addPathData = {};

    vm.getShortestPath = getShortestPath;
    vm.editPath = editPath;
    vm.removePath = removePath;
    vm.addPath = addPath;

    // Init points and list of paths and render graph
    getPoints();
    getPaths();
    renderGraph();

    function getPoints() {
      return PathService.getPoints().then(function (response) {
        vm.points = response;
      }).catch(function (err) {
        console.log(err);
        //switch (err.status) {
        //  case 400:
        //    vm.errorMsg = 'Some values are invalid!';
        //    break;
        //  case 401:
        //    vm.errorMsg = 'Auth token are missing!';
        //    break;
        //  case 500:
        //    vm.erorMsg = err.data;
        //    break;
        //  default:
        //    vm.errorMsg = 'Something wrong...';
        //    break;
        //}
      });
    }

    function getPaths() {
      return PathService.getAllPaths()
        .then(function (response) {
          vm.allPaths = response;
        })
        .catch(function (err) {
          console.log(err);
          //switch (err.status) {
          //  case 400:
          //    vm.errorMsg = 'Some values are invalid!';
          //    break;
          //  case 401:
          //    vm.errorMsg = 'Auth token are missing!';
          //    break;
          //  case 500:
          //    vm.erorMsg = err.data;
          //    break;
          //  default:
          //    vm.errorMsg = 'Something wrong...';
          //    break;
          //}
        });
    }

    function getShortestPath(data) {
      return PathService.getShortestPath(data).then(function (response) {
        console.log(response);
      }).catch(function (err) {
        console.log(err);
        switch (err.status) {
          case 400:
            if (err.data.SecondPoint !== undefined) {
              Alertify.error(err.data.SecondPoint[0]);
            } else {
              Alertify.error(err.data);
            }
            break;
          case 500:
            Alertify.error(err.data);
            break;
          default:
            Alertify.error('Something wrong...');
            break;
        }
      });
    }

    function addPath(data) {
      return PathService.addPath(data).then(function () {
        vm.addPathData = {};
        Alertify.success('Path added successfully!');
        getPoints();
        getPaths();
      }).catch(function (err) {
        console.log(err);
        //switch (err.status) {
        //  case 400:
        //    vm.errorMsg = 'Some values are invalid!';
        //    break;
        //  case 401:
        //    vm.errorMsg = 'Auth token are missing!';
        //    break;
        //  case 500:
        //    vm.erorMsg = err.data;
        //    break;
        //  default:
        //    vm.errorMsg = 'Something wrong...';
        //    break;
        //}
      });
    }

    function editPath(data, id) {
      console.log(data, id);
    }

    function removePath(id, item) {
      return PathService.removePath(id).then(function () {
        var index = vm.allPaths.indexOf(item);
        vm.allPaths.splice(index, 1);
        renderGraph();
        getPoints();
      }).catch(function (err) {
        console.log(err);
        //switch (err.status) {
        //  case 400:
        //    vm.errorMsg = 'Some values are invalid!';
        //    break;
        //  case 401:
        //    vm.errorMsg = 'Auth token are missing!';
        //    break;
        //  case 500:
        //    vm.erorMsg = err.data;
        //    break;
        //  default:
        //    vm.errorMsg = 'Something wrong...';
        //    break;
        //}
      });
    }


    function renderGraph() {
      // IF already render (if you add something or deleting path)
      if (sigmaGraph !== undefined)
        sigmaGraph.kill();

      // Instantiate sigma:
      sigmaGraph = new sigma({
        renderer: {
          container: document.getElementById('graph-container'),
          type: 'canvas'
        },
        settings: {
          labelThreshold: 2,
          doubleClickEnabled: false,
          minEdgeSize: 0.5,
          maxEdgeSize: 3,
          enableEdgeHovering: true,
          edgeHoverColor: 'node',
          defaultEdgeHoverColor: '#062f3c',
          edgeHoverSizeRatio: 1,
          edgeHoverExtremities: true,
          autoCurveRatio: 2
        }
      });

      var points, allPath;
      $q.all([
        PathService.getPoints(),
        PathService.getAllPaths()
      ]).then(function (data) {
        points = data[0];
        allPath = data[1];

        var g = { nodes: [], edges: [] }

        vm.points.forEach(function (item, i) {
          g.nodes.push({
            id: item.name,
            label: item.name,
            x: Math.random(),
            y: Math.random(),
            size: 1.5,
            color: '#062f3c'
          });
        });

        vm.allPaths.forEach(function (item, i) {
          g.edges.push({
            id: i,
            label: item.time.toString(),
            source: item.firstPoint,
            target: item.secondPoint,
            //size: item.time * 3,
            color: '#007ea3'

          });
        });
        sigmaGraph.graph.read(g);
        sigmaGraph.refresh();
      });
    }
  }
})(angular);
