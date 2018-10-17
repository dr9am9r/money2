'use strict';
app.factory('dataService', ['messageCenterService', function (messageCenterService) {

    var dataServiceFactory = {};

    var _validateForm = function (form) {
        angular.forEach(form.$error.required, function (field) {
            field.$setDirty();
        });

        if (form.$invalid) {
            return false;
        }

        return true;
    };

    var _showMessage = function (message) {
        messageCenterService.add('warning', message, { timeout: 3000 });
    };

    var _saveData = function (saveFn, successFn) {
        saveFn()
            .success(function (response) {
                messageCenterService.add('success', 'Изменения успешно сохранены!', { timeout: 3000 });
 
                if (successFn) {
                    successFn(response);
                }
            })
            .error(function (response) {
                messageCenterService.add('danger', 'Ошибка сохранения!', { timeout: 3000 });
            });
    };

    dataServiceFactory.saveData = _saveData;
    dataServiceFactory.showMessage = _showMessage;
    dataServiceFactory.validateForm = _validateForm;

    return dataServiceFactory;
}]);