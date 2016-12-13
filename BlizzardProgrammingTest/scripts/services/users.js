var userServices = angular.module('UserServices', []);

userServices.factory('UserAPI', ['$http', function ($http) {
    function getUserInformation() {
        return new Promise(function (resolve, reject) {
            //$http.get('http://localhost:53653/api/user')
            //    .then(function (response) {
            //        resolve(response.data);
            //    })
            //    .catch(function (err) {
            //        reject(err);
            //    });
            resolve({ username: 'jyurkiw' });
        });
    }

    return {
        getUserInformation: getUserInformation
    }
}]);