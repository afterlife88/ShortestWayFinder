﻿(function (angular) {
  'use strict';

  angular.module('app').controller('HomeController', HomeController);

  HomeController.$inject = ['PathService', '$q'];

  function HomeController(PathService, $q) {

    var vm = this;
    vm.points = [];
    vm.allPaths = [];
    vm.shortestPathData = {};
    vm.getShortestPath = getShortestPath;
    vm.editPath = editPath;
    vm.removePath = removePath;

    getPoints();
    getPaths();
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

    renderGraph();
    function renderGraph() {
      //var n = 1000;
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
        // Instantiate sigma:
        var s = new sigma({
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
            autoCurveRatio: 2

          }
        });
        s.refresh();
      });


    }
  }
})(angular);
