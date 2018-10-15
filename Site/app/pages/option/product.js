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
        for (var i = 0; i < $scope.productData.Products.length; i++) {
            if ($scope.productData.Products[i].TypeId == $scope.typeId) {
                $scope.products.push($scope.productData.Products[i]);
            }
        }

        if ($scope.products.length > 0) {
            $scope.product = $scope.products[0];
        }

        if ($scope.productData.Names.length > 0) {
            $scope.name = $scope.productData.Names[0];
        }
    };

    $scope.save = function () {
        if ($scope.name == '' || $scope.typeId == 0) {
            return;
        }

        var product = {
            Id: 0,
            Name: $scope.name,
            TypeId: $scope.typeId,
            UserId: 0
        };

        $scope.dataService.saveData(
            function () { return productService.save(product); },
            function () {
                $scope.productData.Products.push(product);

                var index = $scope.productData.Names.indexOf($scope.name);
                $scope.productData.Names.splice(index, 1);

                $scope.refresh();
            }
        );
    };

    $scope.delete = function () {
        if ($scope.product.Id == undefined || $scope.product.Id == 0) {
            return;
        }

        productService.delete($scope.product.Id).then(function () {
            $scope.productData.Names.push($scope.product.Name);

            var index = $scope.productData.Products.indexOf($scope.product);
            $scope.productData.Products.splice(index, 1);

            $scope.refresh();
        });
    };

    productService.getProductTypes().then(function (results) {
        $scope.types = results.data;

        if ($scope.types.length > 0) {
            $scope.typeId = $scope.types[0].Id;
        }

        productService.getProducts().then(function (results) {
            $scope.productData = results.data;
            $scope.products = $scope.productData.Products;

            $scope.refresh();
        });
    });
}]);