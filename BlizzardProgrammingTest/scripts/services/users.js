/**
    User Services deal with user data.
*/
var userServices = angular.module('UserServices', []);

/**
    The UserAPI object interacts with the UsersController.

    @namespace UserAPI
*/
userServices.factory('UserAPI', ['$http', function ($http) {

    /**
        Get user data from the backend Users Controller.

        @return Promise Resolves to the user's username from his windows auth token.
    */
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