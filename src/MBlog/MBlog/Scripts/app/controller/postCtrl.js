function PostCtrl($scope, Post) {
    $scope.posts = Post.query(function(posts) {

        angular.forEach(posts);
    });

}