'use strict';

angular.module('project').config(function($routeProvider, $locationProvider) {
    $routeProvider
        .when('/project/:projectId', {
            controller: 'ProjectCtrl',
            templateUrl: '/Scripts/Angular/Project/Views/projectdetails.html'
        })
        .when('/create', {
            controller: 'ProjectCreateCtrl',
            templateUrl: '/Scripts/Angular/Project/Views/create.html'
        })
        .when('/', {
            controller: 'ProjectsListCtrl',
            templateUrl: '/Scripts/Angular/Project/Views/projectlist.html'
        })
        .when('/project/:projectId/task/create', {
            controller: 'TaskCtrl',
            templateUrl: '/Scripts/Angular/Project/Views/createtask.html'
        })
        .otherwise({
            redirectTo: '/'
        });
});