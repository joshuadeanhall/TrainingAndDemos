﻿'use strict';

angular.module('project').controller('ProjectsListCtrl', ['$scope', 'Projects', '$location', function ($scope, Projects, $location) {
    $scope.projects = Projects.query();
    
    $scope.create = function () {
        $location.path('/create');
    }

    $scope.showDetails = function(project) {
        $location.path('/project/' + project.Id);
    }
}]);