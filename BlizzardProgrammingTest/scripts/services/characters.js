var characterServices = angular.module('CharacterServices', []);

characterServices.factory('CharacterAPI', [function() {
    console.log('creating character list service object');

    var getCharactersForUser = function (username) {
        var characterList = [];
        // Query API

        // End query
        // Enter placeholder data (delete tomorrow vvvvvvvvvvvvv)
        // Initialize test data
        characterList.push({
            'id': '1',
            'name': 'Tiger',
            'level': '100',
            'race': 'Human',
            'class': 'Warrior',
            'faction': 'alliance'
        });
        characterList.push({
            'id': '2',
            'name': 'Madman',
            'level': '100',
            'race': 'Troll',
            'class': 'Mage',
            'faction': 'horde'
        });
        // exit placeholder data (delete tomorrow ^^^^^^^^^^^^^^)

        return characterList;
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