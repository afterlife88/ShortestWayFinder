(function (angular) {
  'use strict';

  angular.module('app').controller('HomeController', HomeController);

  HomeController.$inject = ['PathService'];

  function HomeController(PathService) {

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
          console.log(vm.allPaths);
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
  }
})(angular);
