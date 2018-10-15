'use strict';
app.factory('statService', ['$http', 'config', function ($http, config) {
    var serviceBase = config.apiUrl + 'stat';
    var statServiceFactory = {};

    var _getStats = function () {
        return $http.get(serviceBase).then(function (results) {
            return results;
        });
    };

    statServiceFactory.getStats = _getStats;

    return statServiceFactory;
}]);