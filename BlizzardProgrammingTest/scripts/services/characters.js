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

        // Enter placeholder data (delete tomorrow vvvvvvvvvvvvv)
        // Initialize test data
        //characterList.push({
        //    'id': '1',
        //    'name': 'Tiger',
        //    'level': '100',
        //    'race': 'Human',
        //    'class': 'Warrior',
        //    'faction': 'alliance'
        //});
        //characterList.push({
        //    'id': '2',
        //    'name': 'Madman',
        //    'level': '100',
        //    'race': 'Troll',
        //    'class': 'Mage',
        //    'faction': 'horde'
        //});
        // exit placeholder data (delete tomorrow ^^^^^^^^^^^^^^)
    }

    var addCharacterForUser = function (characterData, username) {
        // Create new character

        return true; // Add false check tomorrow
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