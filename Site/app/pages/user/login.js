'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    $scope.loginData = {
        email: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
        if (!$scope.loginForm.$valid) {
            return;
        }

        authService.login($scope.loginData).then(
            function (response) {
                $location.path('/consumption');
            },
            function (error) {
                 $scope.message = error;
            }
        );
    };
}]);