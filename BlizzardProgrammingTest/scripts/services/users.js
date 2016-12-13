var userServices = angular.module('UserServices', []);

userServices.factory('UserAPI', ['$http', function ($http) {
    function getUserInformation() {
        return new Promise(function (resolve, reject) {
            $http.get('http://localhost:53653/api/Users')
                .then(function (response) {
                    resolve(response.data);
                })
                .catch(function (err) {
                    reject(err);
                });
        });
    }

    return {
        getUserInformation: getUserInformation
    };
}]);