'use strict';

angular.module('project').config(function($routeProvider, $locationProvider) {
    $routeProvider
        .when('/Projects', {
            templateUrl: 'projectlist.html'
        })
        .when('/Angular/Example', {
            templateUrl: 'projectlist.html'
        })
        .when('/', {
            templateUrl: '/Scripts/Angular/Project/Views/projectlist.html'
        })
        .otherwise({
            redirectTo: '/phones'
        });
});