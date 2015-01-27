'use strict';

angular.module('project').controller('ProjectCreateCtrl', ['$scope', 'Projects', '$location', function ($scope, Projects, $location) {
    $scope.create = function () {
        Projects.save($scope.project, function() {
            $location.path('/');
        });
        
    }
    $scope.back = function() {
        $location.path('/');
    }
}]);