'use strict';

angular.module('project').controller('ProjectCtrl', ['$scope', 'Projects', '$location', '$routeParams', function ($scope, Projects, $location, $routeParams) {
    $scope.params = $routeParams;
    $scope.project = Projects.get({ id: $routeParams.projectId });
    $scope.create = function () {
        $location.path('/project/' + $routeParams.projectId + '/task/create');
    }
}]);