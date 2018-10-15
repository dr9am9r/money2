'use strict';
app.controller('registerController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {
    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registrationModel = {
        email: "",
        name: "",
        password: "",
        confirmPassword: ""
    };

    $scope.register = function () {
        if (!$scope.registerForm.$valid) {
            return;
        }

        authService.saveRegistration($scope.registrationModel).then(function (response) {
            $scope.savedSuccessfully = true;
            $scope.message = "Регистрация успешно завершена, вы будете перенеправляны на странцу входа через 2 секунды.";
            startTimer();
        },
        function (response) {
            var errors = [];
            for (var key in response.data.modelState) {
                for (var i = 0; i < response.data.modelState[key].length; i++) {
                    errors.push(response.data.modelState[key][i]);
                }
            }
            $scope.message = "Ошибка регистрации: " + errors.join(' ');
        });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }
}]);