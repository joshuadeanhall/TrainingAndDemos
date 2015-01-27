'use strict';

angular.module('project').factory('Tasks', [
    '$resource', function ($resource) {
        var Tasks = $resource('/api/tasks/:id', { id: '@id' }, {

        });
        Tasks.getAll = function () {
            Tasks.query();
        }
        return Tasks;
    }
]);