angular.module('app').config([
  '$routeProvider', '$locationProvider', function ($routeProdiver, $locationProvider) {
    $routeProdiver
      .when('/home',
      {
        controller: 'HomeController'
      })
      .otherwise({
        redirectTo: "/home"
      });
    $locationProvider.hashPrefix('');
  }
]);
