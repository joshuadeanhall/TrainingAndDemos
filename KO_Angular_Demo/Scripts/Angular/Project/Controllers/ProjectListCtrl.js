'use strict';

angular.module('project').controller('ProjectsListCtrl', ['$scope', 'Projects', function ($scope, Projects) {
    alert('gettingAll');
    var projects = Projects.getAll();
    console.log(projects);
}]);