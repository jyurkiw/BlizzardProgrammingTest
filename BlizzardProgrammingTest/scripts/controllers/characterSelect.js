characterApp.controller('character-select-controller', function ($scope, $rootScope) {
    $scope.characterList = [];

    // Character block decoration function
    $scope.characterBlockClass = function (character) {
        return character.faction + '-block-background';
    };

    $scope.characterBlockClickHandler = function (id) {
        $scope.characterList.forEach(function (character, cIndex, arr) {
            if (id === character.id) {
                arr[cIndex].selected = true;
            } else {
                arr[cIndex].selected = false;
            }
        });
    }

    $scope.newCharacterButtonClickHandler = function() {
        $scope.$parent.appState = $scope.appStates.create;
    }

    $scope.deleteCharacterButtonClickHandler = function() {

    }

    $rootScope.$on('ReloadCharacterList', function (event, data) {
        console.log('reloading character list from service');
        
        $scope.characterList.push({
            'id': $scope.characterList.length + 1,
            'name': data.name,
            'level': '1',
            'class': data.class,
            'faction': data.faction
        });
    });

    // Initialize test data
    $scope.characterList.push({
        'id': '1',
        'name': 'Tiger',
        'level': '100',
        'race': 'Human',
        'class': 'Warrior',
        'faction': 'alliance'
    });
    $scope.characterList.push({
        'id': '2',
        'name': 'Madman',
        'level': '100',
        'race': 'Troll',
        'class': 'Mage',
        'faction': 'horde'
    });

    $scope.characterList.forEach(function (character, cIndex, arr) {
        arr[cIndex].selected = false;
    });
});