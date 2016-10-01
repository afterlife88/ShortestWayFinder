angular.module('app').service('spinnerService', ['$rootScope', function ($rootScope) {
  this.showSpinner = function () {

    $rootScope.spinnerShown = true;
  }
  this.hideSpinner = function () {

    $rootScope.spinnerShown = false;
  }
}]);
