'use strict';
app.controller('activeListController', ['$scope', '$route', 'activeService', 'localStorageService', function ($scope, $route, activeService, localStorageService) {

    $scope.actives = [];
    $scope.types = [];
    $scope.editCount = 0;
    $scope.total = 0;
    $scope.filterData = {
        StartDate: '',
        EndDate: ''
    };

    $scope.init = function () {
        $scope.filter();
    }

    $scope.refresh = function () {
        $scope.total = 0;

        for (var i = 0; i < $scope.actives.length; i++) {
            $scope.total += $scope.actives[i].Sum;
        }
    };

    $scope.add = function () {
        $scope.actives.splice(0, 0, {
            Id: 0,
            Date: '',
            Name: '',
            Sum: 0,
            isEdit: true
        });

        $scope.editCount++;
    }

    $scope.edit = function (active) {
        active.isEdit = true;
        $scope.editCount++;
    }

    $scope.save = function (active) {
        $scope.dataService.saveData(
            function () { return activeService.save(active); },
            function () {
                for (var i = 0; i < $scope.types.length; i++) {
                    if ($scope.types[i].Id == active.TypeId) {
                        active.TypeName = $scope.types[i].Name;
                    }
                }

                active.isEdit = false;
                $scope.editCount--;

                $scope.refresh();
            }
        );
    }

    $scope.delete = function (active) {
        var index = $scope.actives.indexOf(active);

        if (active.Id == 0) {
            $scope.actives.splice(index, 1);
            $scope.refresh();
        } else {
            if (confirm('Вы действительно хотите удалить доход?')) {
                activeService.delete(active.Id).then(function () {

                    $scope.actives.splice(index, 1);
                    $scope.refresh();
                });
            }
        }
    }

    $scope.filter = function () {
        activeService.getActives($scope.filterData).then(function (results) {
            $scope.actives = results.data;

            $scope.refresh();
        });
    }

    $scope.clear = function () {
        $scope.filterData = {
            StartDate: '',
            EndDate: '',
            TypeId: 0
        };

        $scope.filter();
    };

    $scope.init();
}]);