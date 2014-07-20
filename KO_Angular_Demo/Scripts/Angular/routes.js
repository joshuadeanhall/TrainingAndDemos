'use strict';

angular.module('project').config(function($routeProvider, $locationProvider) {
    $routeProvider
        .when('/Projects', {
            templateUrl: 'projectlist.html'
        })
        .when('/create', {
            controller: 'ProjectCreateCtrl',
            templateUrl: '/Scripts/Angular/Project/Views/create.html'
        })
        .when('/', {
            controller: 'ProjectsListCtrl',
            templateUrl: '/Scripts/Angular/Project/Views/projectlist.html'
        })
        .otherwise({
            redirectTo: '/phones'
        });
});