'use strict';
app.controller('productController', ['$scope', '$routeParams', 'productService', function ($scope, $routeParams, productService) {

    $scope.productData = {};
    $scope.types = [];
    $scope.products = [];
    $scope.typeId = 0;
    $scope.name = '';
    $scope.product = {};

    $scope.refresh = function () {
        $scope.products = [];
        for (var i = 0; i < $scope.productData.products.length; i++) {
            if ($scope.productData.products[i].typeId == $scope.typeId) {
                $scope.products.push($scope.productData.products[i]);
            }
        }

        if ($scope.products.length > 0) {
            $scope.product = $scope.products[0];
        }

        if ($scope.productData.names.length > 0) {
            $scope.name = $scope.productData.names[0];
        }
    };

    $scope.save = function () {
        if ($scope.name == '' || $scope.typeId == 0) {
            return;
        }

        var product = {
            id: 0,
            name: $scope.name,
            typeId: $scope.typeId,
            userId: 0
        };

        $scope.dataService.saveData(
            function () { return productService.save(product); },
            function () {
                $scope.productData.products.push(product);

                var index = $scope.productData.names.indexOf($scope.name);
                $scope.productData.names.splice(index, 1);

                $scope.refresh();
            }
        );
    };

    $scope.delete = function () {
        if ($scope.product.id == undefined || $scope.product.id == 0) {
            return;
        }

        productService.delete($scope.product.id).then(function () {
            $scope.productData.names.push($scope.product.name);

            var index = $scope.productData.products.indexOf($scope.product);
            $scope.productData.products.splice(index, 1);

            $scope.refresh();
        });
    };

    productService.getProductTypes().then(function (results) {
        $scope.types = results.data;

        if ($scope.types.length > 0) {
            $scope.typeId = $scope.types[0].id;
        }

        productService.getProducts().then(function (results) {
            $scope.productData = results.data;
            $scope.products = $scope.productData.products;

            $scope.refresh();
        });
    });
}]);