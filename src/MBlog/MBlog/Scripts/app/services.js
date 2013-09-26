angular.module('postServices', ['ngResource']).
    factory('Post', function ($resource) {
        return $resource('api/posts/?format=json', {}, {
            query: { method: 'GET', isArray: true }
        });
    });

angular.module('aboutService', ['ngResource']).
    factory('About', function ($resource) {
        return $resource('/api/about/?format=json', {}, {
            query: { method: 'GET', isArray: false }
        });
    });