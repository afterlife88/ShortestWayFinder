// Angular module for the application
angular.module('app', [
  'ngRoute',
  'angularSpinner',
  'xeditable'
]);

angular.module('app').run([
  'editableOptions',
  function (editableOptions) {
    editableOptions.theme = 'bs3';
  }
]);
