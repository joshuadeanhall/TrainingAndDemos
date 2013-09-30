'use strict';

postApp.factory('Post', function($resource) {
    return $resource('api/posts/?format=json', {}, {
        query: { method: 'GET', isArray: true }
    });
});

postApp.factory('About', function ($resource) {
        return $resource('/api/about/?format=json', {}, {
            query: { method: 'GET', isArray: false }
        });
    });