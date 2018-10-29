'use strict';
app.controller('consumptionEditController', ['$scope', '$q', '$routeParams', '$location', 'messageCenterService', 'consumptionService', 'productService', function ($scope, $q, $routeParams, $location, messageCenterService, consumptionService, productService) {
    $scope.consumption = {};
    $scope.id = $routeParams.id;
    $scope.total = 0;
    $scope.editCount = 0;

    $scope.isNew = function () {
        return $routeParams.id == 'new';
    };

    $scope.addItem = function () {
        $scope.consumption.consumptionItems.push({
            id: 0,
            name: '',
            quantity: 1,
            price: 0,
            sum: 0,
            isEdit: true
        });

        $scope.editCount++;
    }

    $scope.editItem = function (item) {
        item.isEdit = true;
        $scope.editCount++;
    }

    $scope.saveItem = function (item, itemForm) {
        if (!$scope.dataService.validateForm(itemForm)) {
            return;
        }

        item.sum = item.quantity * item.price;

        item.isEdit = false;
        $scope.editCount--;

        $scope.refresh();
    }

    $scope.deleteItem = function (item) {
        if (item.isEdit) {
            $scope.editCount--;
        }

        var index = $scope.consumption.consumptionItems.indexOf(item);
        $scope.consumption.consumptionItems.splice(index, 1);
    };

    $scope.save = function (form) {
        if (!$scope.dataService.validateForm(form)) {
            console.log('invalid form');
            return;
        }

        if ($scope.editCount > 0) {
            $scope.dataService.showMessage('Сохраните все элементы!');

            return;
        }

        $scope.dataService.saveData(
            function () { return consumptionService.save($scope.consumption); },
            function () {
                $location.search({}); 
                $location.path('/consumption');
            }
        );
    };

    $scope.refresh = function () {
        $scope.total = 0;

        for (var i = 0; i < $scope.consumption.consumptionItems.length; i++) {
            $scope.consumption.consumptionItems[i].sum = $scope.consumption.consumptionItems[i].quantity * $scope.consumption.consumptionItems[i].price;

            $scope.total += parseFloat($scope.consumption.consumptionItems[i].sum);
        }
    };

    if ($scope.isNew()) {
        $scope.consumption = {
            id: 0,
            date: '',
            place: '',
            sum: 0,
            userId: 0,
            consumptionItems: []
        };

        $scope.addItem();      
    } else {
        consumptionService.getConsumption($scope.id).then(function (result) {
            $scope.consumption = result.data;
            
            $scope.refresh();
        });
    }

    $scope.names = [];
    productService.getProductNames().then(function (result) {
        $scope.names = result.data;
    });

    $scope.places = [];
    productService.getPlaces().then(function (result) {
        $scope.places = result.data;
    });
}]);