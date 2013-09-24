angular.module('postServices', ['ngResource']).
    factory('Post', function ($resource) {
        return $resource('api/posts/?format=json', {}, {
            query: { method: 'GET', isArray: true }
        });
    });