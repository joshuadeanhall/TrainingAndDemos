function AboutMeCtrl($scope, About) {
    $scope.about = About.query();
}