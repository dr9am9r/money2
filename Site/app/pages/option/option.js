'use strict';
app.controller('optionController', ['$scope', '$routeParams', 'authService', function ($scope, $routeParams, authService) {

    $scope.authentication = authService.authentication;
}]);