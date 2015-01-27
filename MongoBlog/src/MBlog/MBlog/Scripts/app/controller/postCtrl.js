'use strict';

postApp.controller('PostCtrl', function PostCtrl($scope, Post) {
    $scope.posts = Post.query(function(posts) {
        angular.forEach(posts);
    });
});

postApp.controller('PostDetailsCtrl', function PostDetailsCtrl($scope, Post) {
    $scope.init = function(id) {
        $scope.id = id;
        $scope.Post = Post.get({ postId: id });
    };
})