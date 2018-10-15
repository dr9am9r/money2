'use strict';
app.factory('incomeService', ['$http', 'config', function ($http, config) {

    var serviceBase = config.apiUrl + 'income';
    var incomeServiceFactory = {};

    var _getIncomes = function (filter) {
        return $http.get(serviceBase + '?typeId=' + filter.TypeId 
                + '&startDate=' + filter.StartDate 
                + '&endDate=' + filter.EndDate)
            .then(function (results) {
                return results;
            });
    };

    var _getIncomeTypes = function () {
        return $http.get(config.apiUrl + 'handbook/incometypes').then(function (results) {
            return results;
        });
    };

    var _delete = function (id) {
        return $http.delete(serviceBase + '/' + id);
    };

    var _save = function (income) {
        return $http.post(serviceBase, income);
    };

    incomeServiceFactory.getIncomes = _getIncomes;
    incomeServiceFactory.getIncomeTypes = _getIncomeTypes;
    incomeServiceFactory.delete = _delete;
    incomeServiceFactory.save = _save;

    return incomeServiceFactory;
}]);