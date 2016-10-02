// Angular module for the application
angular.module('app', [
  'ngRoute',
  'angularSpinner',
  'xeditable',
  'Alertify'
]);

angular.module('app').run([
  'editableOptions',
  function (editableOptions) {
    editableOptions.theme = 'bs3';
  }
]);
