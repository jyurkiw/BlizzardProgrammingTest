var characterServices = angular.module('CharacterServices', []);

characterServices.factory('CharacterAPI', ['$http', function($http) {
    var getCharactersForUser = function (username) {
        // Query API
        return new Promise(function (resolve, reject) {
            $http.get('http://localhost:53653/api/Characters/' + username)
                .then(function (response) {
                    resolve(response.data);
                })
                .catch(function (err) {
                    reject(err);
                });
        });
    }

    var addCharacterForUser = function (characterData, username) {
        return new Promise(function(resolve, reject) {
            // Create new character
            characterData.owner = username;

            $http.post('http://localhost:53653/api/Characters', characterData)
                .then(function (response) {
                    console.log(response);
                    resolve(response.data);
                })
                .catch(function (err) {
                    reject(err);
                });
        })
    }

    var deleteCharacterForUser = function (characterName, username) {
        // Delete character

        return true; // add false check tomorrow
    }

    return {
        getCharactersForUser: getCharactersForUser,
        addCharacterForUser: addCharacterForUser,
        deleteCharacterForUser: deleteCharacterForUser
    }
}]);