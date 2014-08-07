'use strict';

/* jasmine specs for controllers go here */

describe('controllers', function () {
    beforeEach(module('project'));

    function runController($scope, projects, $location) {
        inject(function ($controller) {
            $controller('ProjectsListCtrl', { $scope: $scope, Projects: projects, $location: $location });
        });
    }

    function createMockProjects(projects) {
        return {
            query: function () { return projects}
        }
    }

    it('should attach the list of projects to the scope', function() {
        //spec body
        var $scope = {};
        var $location = {};
        var projectObjects = [ 'proj1', 'proj2' ];
        var projects = createMockProjectList(projectObjects);
        runController($scope, projects, $location);
        expect($scope.projects).toBe(projects);
    });
});