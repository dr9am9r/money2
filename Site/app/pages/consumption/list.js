'use strict';
app.controller('consumptionListController', ['$scope', '$location', '$sce', 'consumptionService', 'productService', 'localStorageService', function ($scope, $location, $sce, consumptionService, productService, localStorageService) {

    $scope.consumptions = [];
    $scope.types = [];
    $scope.editCount = 0;
    $scope.total = 0;
    $scope.filterData = {
        startDate: '',
        endDate: '',
        typeId: 0
    };

    $scope.init = function () {
        productService.getProductTypes().then(function (result) {
            $scope.types = result.data;
            $scope.types.unshift( { id: 0, name: 'Все' } );

            var filter = localStorageService.get('ConsumptionFilter');
            if (filter) {
                $scope.filterData = filter;
            }

            if ($scope.filterData.startDate === '' && $scope.filterData.endDate === '' && $scope.filterData.typeId == 0)
            {
                var today = new Date();
                var mm = today.getMonth() + 1;
                var yyyy = today.getFullYear();

                if (mm < 10) {
                    mm = '0' + mm
                }

                today = yyyy + '-' + mm + '-01';

                $scope.filterData.startDate = today;
            }

            if ($location.search().startDate) {
                $scope.filterData.startDate = $location.search().startDate;
            }

            if ($location.search().endDate) {
                $scope.filterData.endDate = $location.search().endDate;
            }
            
            if ($location.search().typeId) {
                $scope.filterData.typeId = parseInt($location.search().typeId);
            }

            $scope.filter();
        });
    }

    $scope.refresh = function () {
        $scope.total = 0;

        for (var i = 0; i < $scope.consumptions.length; i++) {
            $scope.total += $scope.consumptions[i].sum;

            $scope.consumptions[i].tooltip = $scope.getTooltip($scope.consumptions[i]);
        }
    };

    $scope.delete = function (consumption) {
        if (confirm('Вы действительно хотите удалить доход?')) {
            consumptionService.delete(consumption.id).then(function () {
                var index = $scope.consumptions.indexOf(consumption);
                $scope.consumptions.splice(index, 1);
                $scope.refresh();
            });
        }
    }

    $scope.filter = function () {

        localStorageService.set('ConsumptionFilter', $scope.filterData);

        consumptionService.getConsumptions($scope.filterData).then(function (results) {
            $scope.consumptions = results.data;

            $scope.refresh();
        });
    }

    $scope.clear = function () {
        $scope.filterData = {
            startDate: '',
            endDate: '',
            typeId: 0
        };

        var today = new Date();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();

        if (mm < 10) {
            mm = '0' + mm
        }

        today = yyyy + '-' + mm + '-01';

        $scope.filterData.startDate = today;

        $scope.filter();
    };

    $scope.getTooltip = function (consumption) {
        var html = '<table class="t-tooltip">';
        for (var i = 0; i < consumption.consumptionItems.length; i++) {
            html += '<tr>';
            html += '<td style="text-align: left;">' + consumption.consumptionItems[i].name + '</td>';
            html += '<td class="price">' + (consumption.consumptionItems[i].quantity * consumption.consumptionItems[i].price).toFixed(2) + 'р.</td>';
            html += '</tr>';
        }
        html += '</table>';

        return $sce.trustAsHtml(html);
    };

    $scope.init();
}]);