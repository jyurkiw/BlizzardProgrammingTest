/**
   Character services deal with Character data.
*/
var characterServices = angular.module('CharacterServices', []);

/**
    The character API object interacts with the character controller in the API.
    It does most of the heavy lifting on the character select page and is posted
    to on the character creation page.

    @namespace CharacterAPI
*/
characterServices.factory('CharacterAPI', ['$http', function($http) {
    /**
        Get a list of characters owned by the user.
        Matches characters based on passed username.

        @memberof CharacterAPI
        @param string username The name of the user.
        @return Promise The API response data.
    */
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

    /**
        Get a list of deleted characters owned by the user.
        Matches characters based on passed username.

        @memberof CharacterAPI
        @param string username The name of the user.
        @return Promise The API response data.
    */
    var getDeletedCharactersForUser = function (username) {
        // Query API
        return new Promise(function (resolve, reject) {
            $http.get('http://localhost:53653/api/DeletedCharacters/' + username)
                .then(function (response) {
                    resolve(response.data);
                })
                .catch(function (err) {
                    reject(err);
                });
        });
    }

    /**
        Submit a character for insertion for the user.

        @memberof CharacterAPI
        @param Object characterData The character data.
        @param string username The name of the user.
        @return Promise A promise that resolves to the response data (should be a standard OK message).

    */
    var addCharacterForUser = function (characterData, username) {
        return new Promise(function (resolve, reject) {
            // Create new character
            characterData.owner = username;

            $http.post('http://localhost:53653/api/Characters', characterData)
                .then(function (response) {
                    resolve(response.data);
                })
                .catch(function (err) {
                    reject(err);
                });
        });
    }

    /**
        Delete a character for the user.

        @memberof CharacterAPI
        @param Int The character ID.
        @return Promise A promise that resolves to the response data (should be a standard OK message).
    */
    var deleteCharacterForUser = function (characterId) {
        return new Promise(function (resolve, reject) {
            $http.delete('http://localhost:53653/api/Characters/' + characterId)
                .then(function (response) {
                    resolve(response.data);
                })
                .catch(function (err) {
                    reject(err);
                });
        });
    }

    /**
        Undelete a character for the user.

        @memberof CharacterAPI
        @param Int The character ID.
        @return Promise A promise that resolves to the response data (should be a standard OK message).
    */
    var undeleteCharacterForUser = function (characterId) {
        return new Promise(function (resolve, reject) {
            $http.post('http://localhost:53653/api/DeletedCharacters/' + characterId)
                .then(function (response) {
                    resolve(response.data);
                })
                .catch(function (err) {
                    reject(err);
                });
        });
    }

    return {
        getCharactersForUser: getCharactersForUser,
        getDeletedCharactersForUser: getDeletedCharactersForUser,
        addCharacterForUser: addCharacterForUser,
        deleteCharacterForUser: deleteCharacterForUser,
        undeleteCharacterForUser: undeleteCharacterForUser
    }
}]);