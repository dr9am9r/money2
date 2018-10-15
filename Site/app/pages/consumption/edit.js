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
        $scope.consumption.ConsumptionItems.push({
            Id: 0,
            Name: '',
            Quantity: 1,
            Price: 0,
            Sum: 0,
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

        item.Sum = item.Quantity * item.Price;

        item.isEdit = false;
        $scope.editCount--;

        $scope.refresh();
    }

    $scope.deleteItem = function (item) {
        if (item.isEdit) {
            $scope.editCount--;
        }

        var index = $scope.consumption.ConsumptionItems.indexOf(item);
        $scope.consumption.ConsumptionItems.splice(index, 1);
    };

    $scope.save = function (form) {
        if (!$scope.dataService.validateForm(form)) {
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

        for (var i = 0; i < $scope.consumption.ConsumptionItems.length; i++) {
            $scope.consumption.ConsumptionItems[i].Sum = $scope.consumption.ConsumptionItems[i].Quantity * $scope.consumption.ConsumptionItems[i].Price;

            $scope.total += parseFloat($scope.consumption.ConsumptionItems[i].Sum);
        }
    };

    if ($scope.isNew()) {
        $scope.consumption = {
            Id: 0,
            Date: '',
            Place: '',
            Sum: 0,
            UserId: 0,
            ConsumptionItems: []
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