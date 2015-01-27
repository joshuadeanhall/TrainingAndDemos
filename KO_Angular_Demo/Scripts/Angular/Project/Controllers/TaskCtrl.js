'use strict';

angular.module('project').controller('TaskCtrl', ['$scope', 'Tasks', '$location', '$routeParams', function ($scope, Tasks, $location, $routeParams) {
    $scope.task = new Object();
    $scope.task.projectId = $routeParams.projectId;
    $scope.routeParams = $routeParams;
    $scope.create = function () {
        Tasks.save($scope.task, function () {
            $location.path('/project/' + $scope.routeParams.projectId);
        });
    }
    $scope.back = function() {
        $location.path('/project/' + $scope.routeParams.projectId);
    }
}]);