var app = angular.module('MoneyApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'smart-table', 'angular.filter', 'ui.bootstrap', 'MessageCenterModule', 'templates-main', 'chart.js']);

app.config(['$routeProvider', function($routeProvider){

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "../app/pages/home/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "../app/pages/user/login.html"
    });

    $routeProvider.when("/register", {
        controller: "registerController",
        templateUrl: "../app/pages/user/register.html"
    });

    $routeProvider.when("/option", {
        controller: "optionController",
        templateUrl: "../app/pages/option/option.html"
    });

    $routeProvider.when("/product", {
        controller: "productController",
        templateUrl: "../app/pages/option/product.html"
    });

    $routeProvider.when("/income", {
        controller: "incomeListController",
        templateUrl: "../app/pages/income/list.html"
    });

    $routeProvider.when("/consumption", {
        controller: "consumptionListController",
        templateUrl: "../app/pages/consumption/list.html"
    });

    $routeProvider.when("/consumption/:id", {
        controller: "consumptionEditController",
        templateUrl: "../app/pages/consumption/edit.html"
    });

    $routeProvider.when("/stat", {
        controller: "statController",
        templateUrl: "../app/pages/stat/stat.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
}]);

app.run(['authService', 'dataService', '$rootScope', function (authService, dataService, $rootScope) {
    authService.fillAuthData();

    $rootScope.dataService = dataService;
}]);

app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
}]);

app.constant('config', {
    appName: 'Money',
    appVersion: 0.1,
    //apiUrl: 'http://money.you13.ru/service/api/',
    //tokenUrl: 'http://money.you13.ru/service/token'
    apiUrl: 'http://localhost:59816/api/',
    tokenUrl: 'http://localhost:59816/api/auth/login'
});