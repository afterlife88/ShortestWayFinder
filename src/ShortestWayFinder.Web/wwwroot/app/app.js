// Angular module for the application
angular.module('app', [
  'ngRoute',
  'angularSpinner'
]);

angular.module('app').run([
  '$rootScope', '$location',
  function ($rootScope, $location) {
  }
]);
