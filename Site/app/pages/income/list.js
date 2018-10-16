'use strict';
app.controller('incomeListController', ['$scope', '$route', 'incomeService', 'localStorageService', function ($scope, $route, incomeService, localStorageService) {

    $scope.incomes = [];
    $scope.types = [];
    $scope.editCount = 0;
    $scope.total = 0;
    $scope.filterData = {
        startDate: '',
        endDate: '',
        typeId: 0
    };

    $scope.init = function () {
        $scope.filter();
    }

    incomeService.getIncomeTypes().then(function (result) {
        $scope.types = result.data;
    });

    $scope.refresh = function () {
        $scope.total = 0;

        for (var i = 0; i < $scope.incomes.length; i++) {
            $scope.total += $scope.incomes[i].sum;
        }
    };

    $scope.add = function () {
        $scope.incomes.splice(0, 0, {
            id: 0,
            date: '',
            name: '',
            typeId: 0,
            sum: 0,
            isEdit: true
        });

        $scope.editCount++;
    }

    $scope.edit = function (income) {
        income.isEdit = true;
        $scope.editCount++;
    }

    $scope.save = function (income) {
        $scope.dataService.saveData(
            function () { return incomeService.save(income); },
            function () {
                for (var i = 0; i < $scope.types.length; i++) {
                    if ($scope.types[i].id == income.typeId) {
                        income.typeName = $scope.types[i].name;
                    }
                }

                income.isEdit = false;
                $scope.editCount--;

                $scope.refresh();
            }
        );
    }

    $scope.delete = function (income) {
        var index = $scope.incomes.indexOf(income);

        if (income.id == 0) {
            $scope.incomes.splice(index, 1);
            $scope.refresh();
        } else {
            if (confirm('Вы действительно хотите удалить доход?')) {
                incomeService.delete(income.id).then(function () {
                    $scope.incomes.splice(index, 1);
                    $scope.refresh();
                });
            }
        }
    }

    $scope.filter = function () {
        incomeService.getIncomes($scope.filterData).then(function (results) {
            $scope.incomes = results.data;

            $scope.refresh();
        });
    }

    $scope.clear = function () {
        $scope.filterData = {
            startDate: '',
            endDate: '',
            typeId: 0
        };

        $scope.filter();
    };

    $scope.init();
}]);