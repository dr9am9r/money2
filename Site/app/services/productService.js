'use strict';
app.factory('productService', ['$http', 'config', function ($http, config) {

    var serviceBase = config.apiUrl + 'product';
    var productServiceFactory = {};

    var _getProducts = function () {
        return $http.get(serviceBase).then(function (results) {
            return results;
        });
    };

    var _getPlaces = function () {
        return $http.get(config.apiUrl + 'handbook/places').then(function (results) {
            return results;
        });
    };

    var _getProductNames = function () {
        return $http.get(config.apiUrl + 'handbook/productnames').then(function (results) {
            return results;
        });
    };

    var _getProductTypes = function () {
        return $http.get(config.apiUrl + 'handbook/producttypes').then(function (results) {
            return results;
        });
    };

    var _delete = function (id) {
        return $http.delete(serviceBase + '/' + id);
    };

    var _save = function (product) {
        return $http.post(serviceBase, product);
    };

    productServiceFactory.getPlaces = _getPlaces;
    productServiceFactory.getProducts = _getProducts;
    productServiceFactory.getProductNames = _getProductNames;
    productServiceFactory.getProductTypes = _getProductTypes;
    productServiceFactory.delete = _delete;
    productServiceFactory.save = _save;

    return productServiceFactory;
}]);