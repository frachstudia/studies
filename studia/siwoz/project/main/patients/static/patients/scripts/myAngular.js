

'use strict';


var restApp = angular.module('restApp', [
  'ngRoute',
  'restAppController'
]);

restApp.config(['$routeProvider',
  function($routeProvider) {
    $routeProvider.
      when('/news', {
        templateUrl: 'patients/templates/patients/mainSite.html',
        controller: 'CategoryListCtrl'
      }).
      otherwise({
        redirectTo: '/'
      });
  }]);

