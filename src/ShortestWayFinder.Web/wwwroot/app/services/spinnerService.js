angular.module('app').service('spinnerService', ['$rootScope', 'usSpinnerService', function ($rootScope, usSpinnerService) {
  this.showSpinner = function () {

    $rootScope.spinnerShown = true;
  }
  this.hideSpinner = function () {
  
    $rootScope.spinnerShown = false;
  }
}]);
