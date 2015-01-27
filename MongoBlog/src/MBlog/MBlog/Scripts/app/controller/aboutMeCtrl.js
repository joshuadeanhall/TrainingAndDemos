'use strict';

postApp.controller('AboutMeCtrl', function AboutMeCtrl($scope, About) {
    $scope.about = About.query();
});