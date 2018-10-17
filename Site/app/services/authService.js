'use strict';
app.factory('authService', ['$http', '$q', 'config', 'localStorageService', function ($http, $q, config, localStorageService) {

    var serviceBase = config.apiUrl + 'auth';
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        email: "",
        name: ""
    };

    var _saveRegistration = function (registration) {

        _logOut();

        return $http.post(serviceBase + '/register', registration).then(function (response) {
            return response;
        });

    };

    var _login = function (loginData) {

        var data = { Email: loginData.email, Password: loginData.password }; // "grant_type=password&username=" + loginData.email + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(config.tokenUrl, data).success(function (response) {
            localStorageService.set('authorizationData', { token: response.token, email: loginData.email });

            _fillAuthData();

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.email = "";
        _authentication.name = "";

    };

    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            $http.get(config.apiUrl + 'user').then(function (results) {
                _authentication.name = results.data.name;
            });

            _authentication.isAuth = true;
            _authentication.email = authData.email;
        }
    }

    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;

    return authServiceFactory;
}]);