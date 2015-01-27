angular.module('testApp', [])
    .controller('saveCtrl', [
        '$scope', '$http', function ($scope, $http) {
            $scope.save = function () {

                $http.post('/api/user', { Name: 'helloword!', Password: 'test123', Age: 't3s' }).then(function(response) {
                    var data = response.data,
                        status = response.status,
                        header = response.header,
                        config = response.config;
                }, function(response) {
                    var data = response.data,
                        status = response.status,
                        header = response.header,
                        config = response.config;
                });

                // Simple POST request example (passing data) :
                $http.post('/api/user', { Name: 'josh', Password: 'test123', age: 'test3' }).
                  success(function (data, status, headers, config) {
                      // this callback will be called asynchronously
                      // when the response is available
                        debugger;
                        alert('success');

                    }).error(function (data, status, headers, config) {
                      // called asynchronously if an error occurs
                      // or server returns response with an error status.
                    alert('fail');
                });
            }
        }
    ]);