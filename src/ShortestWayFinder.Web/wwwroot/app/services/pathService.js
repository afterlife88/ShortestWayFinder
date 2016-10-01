(function () {
  'use strict';

  angular.module('app').factory('PathService', PathService);

  PathService.$inject = ['$http', '$q', 'spinnerService'];

  function PathService($http, $q, spinnerService) {

    var pathService = {
      getPoints: getPoints,
      getShortestPath: getShortestPath,
      getAllPaths: getAllPaths
    };

    return pathService;

    function getPoints() {
      spinnerService.showSpinner();
      return $http.get('api/path/points')
        .then(function (response) {
          spinnerService.hideSpinner();
          return response.data;
        }).catch(function (data) {
          spinnerService.hideSpinner();
          return $q.reject(data);
        });
    }

    function getShortestPath(data) {
      spinnerService.showSpinner();
      return $http.post('api/path/shortestpath', data)
      .then(function (response) {
        spinnerService.hideSpinner();
        return response.data;
      }).catch(function (data) {
        spinnerService.hideSpinner();
        return $q.reject(data);
      });
    }

    function getAllPaths() {
      spinnerService.showSpinner();
      return $http.get('api/path')
      .then(function (response) {
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
    //function getUserByEmail(email, token) {
    //  spinner.showWait();
    //  return $http.get('api/User/getUser/' + email,
    //  {
    //    headers: { 'Authorization': token }
    //  }).then(function (response) {
    //    spinner.hideWait();
    //    return response;
    //  }).catch(function (data) {
    //    spinner.hideWait();
    //    return $q.reject(data);
    //  });
    //}
    //function getUserById(userId) {
    //  spinner.showWait();
    //  return $http.get('api/User/' + userId
    //  ).then(function (response) {
    //    spinner.hideWait();
    //    return response;
    //  }).catch(function (data) {
    //    spinner.hideWait();
    //    return $q.reject(data);
    //  });
    //}

  }
})();
