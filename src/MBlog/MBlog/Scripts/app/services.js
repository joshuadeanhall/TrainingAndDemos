'use strict';

postApp.factory('Post', function($resource) {
    return $resource('/api/posts/:postId?format=json', {}, {
        
    });
});

postApp.factory('About', function ($resource) {
        return $resource('/api/about/?format=json', {}, {
            query: { method: 'GET', isArray: false }
        });
    });