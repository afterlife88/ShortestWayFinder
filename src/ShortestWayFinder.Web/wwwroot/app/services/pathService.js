(function () {
  'use strict';

  angular.module('app').factory('PathService', PathService);

  PathService.$inject = ['$http', '$q'];

  function PathService($http, $q, spinner) {

    var pathService = {
      create: create,
      getUserByEmail: getUserByEmail,
      getUserById: getUserById,
      update: update
    };

    return pathService;

    function create(user) {
      spinner.showWait();
      return $http.post('api/user/register', user)
        .then(function (response) {
          spinner.hideWait();
          return response.data;
        }).catch(function (data) {
          spinner.hideWait();
          return $q.reject(data);
        });
    }
    function update(user, token) {
      spinner.showWait();
      return $http.put('api/user/update', user, {
        headers: { 'Authorization': token }
      }).then(function (response) {
        spinner.hideWait();
        return response;
      }).catch(function (data) {
        spinner.hideWait();
        return $q.reject(data);
      });
    }
    function getUserByEmail(email, token) {
      spinner.showWait();
      return $http.get('api/User/getUser/' + email,
      {
        headers: { 'Authorization': token }
      }).then(function (response) {
        spinner.hideWait();
        return response;
      }).catch(function (data) {
        spinner.hideWait();
        return $q.reject(data);
      });
    }
    function getUserById(userId) {
      spinner.showWait();
      return $http.get('api/User/' + userId
      ).then(function (response) {
        spinner.hideWait();
        return response;
      }).catch(function (data) {
        spinner.hideWait();
        return $q.reject(data);
      });
    }

  }
})();
