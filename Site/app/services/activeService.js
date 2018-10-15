'use strict';
app.factory('activeService', ['$http', 'config', function ($http, config) {

    var serviceBase = config.apiUrl + 'active';
    var activeServiceFactory = {};

    var _getActives = function (filter) {
        return $http.get(serviceBase + '?startDate=' + filter.StartDate 
                + '&endDate=' + filter.EndDate)
            .then(function (results) {
                return results;
            });
    };

    var _delete = function (id) {
        return $http.delete(serviceBase + '/' + id);
    };

    var _save = function (income) {
        return $http.post(serviceBase, income);
    };

    activeServiceFactory.getActives = _getActives;
    activeServiceFactory.delete = _delete;
    activeServiceFactory.save = _save;

    return activeServiceFactory;
}]);