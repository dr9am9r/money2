'use strict';
app.controller('statController', ['$scope', '$routeParams', 'statService', function ($scope, $routeParams, statService) {

    $scope.stats = {};
    $scope.ctotal = 0;
    $scope.ctotal1 = 0;
    $scope.ctotal2 = 0;
    $scope.ctotal3 = 0;
    $scope.itotal = 0;
    $scope.atotal = 0;
    $scope.charts = { mode: '0', isLine: false, isBar: false };

    $scope.getStyle = function (color) {
        if (color) {
            return "font-weight: bold; color: " + color;
        }

        return "";
    };

    $scope.getPercent = function (price, total) {
        if (total == 0) {
            return 0;
        }

        return (price / total * 100).toFixed(2);
    };

    $scope.updateCharts = function () {
        $scope.labels = [];
        $scope.series = [];
        $scope.data = [];
        $scope.charts.isBar = false;
        $scope.charts.isLine = false;

        switch ($scope.charts.mode) {
            case '0':
                $scope.charts.isLine = true;

                $scope.labels = $scope.stats.Charts[0].Labels;
                for (var i = 0; i < $scope.stats.Charts[0].Series.length; i++) {
                    $scope.series.push($scope.stats.Charts[0].Series[i].Name);
                    $scope.data.push($scope.stats.Charts[0].Series[i].Points);
                }

                break;
            case '1':
                $scope.charts.isBar = true;

                $scope.labels = $scope.stats.Charts[1].Labels;
                for (var i = 0; i < $scope.stats.Charts[1].Series.length; i++) {
                    $scope.series.push($scope.stats.Charts[1].Series[i].Name);
                    $scope.data.push($scope.stats.Charts[1].Series[i].Points);
                }

                break;
            case '2':
                $scope.charts.isBar = true;

                $scope.labels = $scope.stats.Charts[2].Labels;
                for (var i = 0; i < $scope.stats.Charts[2].Series.length; i++) {
                    $scope.series.push($scope.stats.Charts[2].Series[i].Name);
                    $scope.data.push($scope.stats.Charts[2].Series[i].Points);
                }

                break;
            default:
                break;
        };
    };

    function CalcMax(max, item, count)
    {
        for (var i = 0; i < 3; i++) {
            if (!max[i]) {
                max[i] = item;
                return;
            }

            if (max[i].Sums[count] < item.Sums[count]) {
                var temp = item;
                item = max[i];
                max[i] = temp;
            }
        }
    }

    statService.getStats().then(function (results) {
        $scope.stats = results.data;

        var max = [];
        max[0] = [];
        max[1] = [];
        max[2] = [];

        for (var i = 0; i < $scope.stats.ProductStats.length; i++) {
            $scope.ctotal += $scope.stats.ProductStats[i].Total;
            $scope.ctotal1 += $scope.stats.ProductStats[i].Sums[0];
            $scope.ctotal2 += $scope.stats.ProductStats[i].Sums[1];
            $scope.ctotal3 += $scope.stats.ProductStats[i].Sums[2];
            $scope.stats.ProductStats[i].Color = [];

            CalcMax(max[0], $scope.stats.ProductStats[i], 0);
            CalcMax(max[1], $scope.stats.ProductStats[i], 1);
            CalcMax(max[2], $scope.stats.ProductStats[i], 2);
        }

        for (var i = 0; i <= 2; i++) {
            max[i][0].Color[i] = "red";
            max[i][1].Color[i] = "darkorange";
            max[i][2].Color[i] = "gold";
        }

        for (var i = 0; i < $scope.stats.IncomeStats.length; i++) {
            $scope.itotal += $scope.stats.IncomeStats[i].Sum;
        }

        for (var i = 0; i < $scope.stats.ActiveStats.length; i++) {
            $scope.atotal += $scope.stats.ActiveStats[i].Sum;
        }

        $scope.updateCharts();
    });

    $scope.lineOptions = {
        legend: {
            display: true
        },
        tooltips: {
            mode: 'single'
        }
    };
    
    $scope.barOptions = {
        scales:{
            xAxes: [{
                stacked: true
            }],
            yAxes: [{
                stacked: true
            }]
        },
        legend: {
            display: true
        },
        tooltips: {
            mode: 'single'
        }
    };
}]);