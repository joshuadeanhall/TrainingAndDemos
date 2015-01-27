'use strict';

angular.module('project').factory('Projects', [
    '$resource', function($resource) {
        var Projects = $resource('/api/projects/:id', {id:'@id'}, {

        });
        Projects.getAll = function() {
            Projects.query();
        }
        return Projects;
    }
]);