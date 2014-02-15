'use strict';

angular.module('messageSenderApp').controller('MessageController',
    function($scope) {
        $scope.sendMessage = function() {
            alert('hi there');
        };
    });