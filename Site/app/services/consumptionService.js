'use strict';
app.factory('consumptionService', ['$http', 'config', function ($http, config) {

    var serviceBase = config.apiUrl + 'consumption';
    var consumptionServiceFactory = {};

    var _getConsumptions = function (filter) {
        return $http.get(serviceBase + '?typeId=' + filter.TypeId
                + '&startDate=' + filter.StartDate
                + '&endDate=' + filter.EndDate)
            .then(function (results) {
                return results;
            });
    };

    var _getConsumption = function (id) {
        return $http.get(serviceBase + '/' + id).then(function (result) {
            return result;
        });
    };

    var _delete = function (id) {
        return $http.delete(serviceBase + '/' + id);
    };

    var _save = function (income) {
        return $http.post(serviceBase, income);
    };

    consumptionServiceFactory.getConsumptions = _getConsumptions;
    consumptionServiceFactory.getConsumption = _getConsumption;
    consumptionServiceFactory.delete = _delete;
    consumptionServiceFactory.save = _save;

    return consumptionServiceFactory;
}]);