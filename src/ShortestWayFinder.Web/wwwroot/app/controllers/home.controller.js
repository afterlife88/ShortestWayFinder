(function (angular) {
  'use strict';

  angular.module('app').controller('HomeController', HomeController);

  HomeController.$inject = ['PathService', '$q', 'Alertify'];

  function HomeController(PathService, $q, Alertify) {

    var vm = this;
    var sigmaGraph, sigmaShortestPathGraph;
    vm.points = [];
    vm.allPaths = [];
    vm.shortestPathData = {};
    vm.addPathData = {};
    vm.backgroundColorShortestPath = '';
    vm.showResultOfShortestPath = false;
    vm.resultShortPath = {
      estimatedTime: 0,
      path: []
    }


    vm.getShortestPath = getShortestPath;
    vm.editPath = editPath;
    vm.removePath = removePath;
    vm.addPath = addPath;

    // Init points and list of paths and render graph
    getPoints();
    getPaths();

    function getPoints() {
      return PathService.getPoints().then(function (response) {
        vm.points = response;
      }).catch(function (err) {
        Alertify.error(err.data);
      });
    }

    function getPaths() {
      return PathService.getAllPaths().then(function (response) {
        var arrNodes = [];
        response.forEach(function (item) {
          arrNodes.push(item.firstPoint);
          arrNodes.push(item.secondPoint);
        });
        vm.allPaths = response;
        renderGraph(unique(arrNodes), response);
      }).catch(function (err) {
        Alertify.error(err.data);
      });
    }

    function getShortestPath(data) {
      vm.resultShortPath = {
        estimatedTime: 0,
        path: []
      }
      return PathService.getShortestPath(data).then(function (response) {
        vm.backgroundColorShortestPath = 'rgba(95, 109, 133, 0.8)';
        var arrNodes = [];

        response.forEach(function (item) {
          arrNodes.push(item.firstPoint);
          arrNodes.push(item.secondPoint);
          vm.resultShortPath.estimatedTime += item.time;
        });

        vm.resultShortPath.path = response;
        vm.showResultOfShortestPath = true;
        renderShortestPathGraph(unique(arrNodes), response);

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
        switch (err.status) {
          case 400:
            if (err.data.SecondPoint !== undefined) {
              Alertify.error(err.data.SecondPoint[0]);
              if (err.data.SecondPoint[1])
                Alertify.error(err.data.SecondPoint[1]);
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

    function editPath(data, id) {
      var d = $q.defer();

      data.Id = id;
      return PathService.updatePath(data).then(function () {
        Alertify.success('Path updated successfully!');
        getPoints();
        getPaths();
        d.resolve();
      }).catch(function (err) {
        console.log(err);
        switch (err.status) {
          case 400:
            if (err.data.SecondPoint !== undefined) {
              d.resolve(err.data.SecondPoint[0]);
              Alertify.error(err.data.SecondPoint[0]);
            } else {
              Alertify.error(err.data);
              d.resolve(err.data);
            }
            break;
          case 500:
            d.resolve(err.data);
            Alertify.error(err.data);
            break;
          default:
            Alertify.error('Something wrong...');
            break;
        }
        return d.promise;
      });

    }


    function removePath(id, item) {
      return PathService.removePath(id).then(function () {
        var index = vm.allPaths.indexOf(item);
        vm.allPaths.splice(index, 1);
        getPoints();
        getPaths();
      }).catch(function (err) {
        console.log(err);
        switch (err.status) {
          case 404:
            Alertify.error('Requested path not found!');
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


    function renderShortestPathGraph(arrNodes, arrPath) {
      if (sigmaShortestPathGraph !== undefined)
        sigmaShortestPathGraph.kill();

      var g = { nodes: [], edges: [] }

      arrNodes.forEach(function (item, i) {
        g.nodes.push({
          id: item,
          label: item,
          x: i * 35,
          y: i * 0,
          size: 1.5,
          color: 'white'
        });
      });

      arrPath.forEach(function (item, i) {
        g.edges.push({
          id: i,
          label: item.time.toString(),
          source: item.firstPoint,
          target: item.secondPoint,
          //size: item.time * 3,
          color: 'white',
          type: ['arrow']

        });
      });

      sigmaShortestPathGraph = new sigma({
        graph: g,
        renderer: {
          container: document.getElementById('shortestPath-graph-container'),
          type: 'canvas'
        },
        settings: {
          labelAlignment: 'top',
          defaultLabelColor: 'white',
          labelColor: 'node',
          labelSizeRatio: '3',
          labelThreshold: 2,
          minEdgeSize: 8,
          maxEdgeSize: 8,
          minNodeSize: 9,
          maxNodeSize: 17,
          nodesPowRatio: 0.3,
          edgesPowRatio: 0.3,
          enableEdgeHovering: true,
          edgeHoverColor: 'node',
          defaultEdgeHoverColor: 'white',
          edgeHoverExtremities: true,
          edgeHoverSizeRatio: 1,
          doubleClickEnabled: true,
          mouseEnabled: false
        }
      });
      sigmaShortestPathGraph.refresh();
      sigmaShortestPathGraph.cameras[0].goTo({ x: 0, y: 0, angle: 0, ratio: 1.2 });
    }

    function renderGraph(arrNodes, arrPath) {
      // IF already render (if you add something or deleting path)
      if (sigmaGraph !== undefined)
        sigmaGraph.kill();


      var g = { nodes: [], edges: [] }

      arrNodes.forEach(function (item, i) {
        g.nodes.push({
          id: item,
          label: item,
          x: Math.cos(i * 2 * Math.PI * 100 / Math.pow(vm.points.length, 3)),
          y: Math.sin(i * 2 * Math.PI * 100 / Math.pow(vm.points.length, 3)),

          size: 1.5,
          color: '#062f3c'
        });
      });

      arrPath.forEach(function (item, i) {
        g.edges.push({
          id: i,
          label: item.time.toString(),
          source: item.firstPoint,
          target: item.secondPoint,
          //size: item.time * 3,
          color: '#007ea3'

        });
      });
      sigmaGraph = new sigma({
        graph: g,
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
          autoCurveRatio: 2,
          zoomingRatio: 1.2
        }
      });
      // Instantiate sigma:
      sigmaGraph.refresh();
    }
  }
})(angular);
