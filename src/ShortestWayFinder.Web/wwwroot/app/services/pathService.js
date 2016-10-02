(function () {
  'use strict';

  angular.module('app').factory('PathService', PathService);

  PathService.$inject = ['$http', '$q', 'spinnerService'];

  function PathService($http, $q, spinnerService) {

    var pathService = {
      getPoints: getPoints,
      getShortestPath: getShortestPath,
      getAllPaths: getAllPaths,
      removePath: removePath,
      addPath: addPath
    };

    return pathService;

    function getPoints() {
      spinnerService.showSpinner();
      return $http.get('api/path/points').then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }

    function getShortestPath(data) {
      spinnerService.showSpinner();
      return $http.post('api/path/shortestpath', data).then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }

    function getAllPaths() {
      spinnerService.showSpinner();
      return $http.get('api/path').then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }

    function removePath(id) {
      spinnerService.showSpinner();
      return $http.delete('api/path/' + id).then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }

    function addPath(data) {
      spinnerService.showSpinner();
      return $http.post('api/path/add', data).then(function(response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }
    //function update(user, token) {
    //  spinner.showWait();
    //  return $http.put('api/user/update', user, {
    //    headers: { 'Authorization': token }
    //  }).then(function (response) {
    //    spinner.hideWait();
    //    return response;
    //  }).catch(function (data) {
    //    spinner.hideWait();
    //    return $q.reject(data);
    //  });
    //}
  }
})();
