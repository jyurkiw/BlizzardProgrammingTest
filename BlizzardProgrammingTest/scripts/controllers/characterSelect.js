characterApp.controller('character-select-controller', function ($scope, $rootScope, CharacterAPI) {
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
        LoadCharacterList();
    });

    $rootScope.$on('UsernameLoaded', function () {
        LoadCharacterList();
    });

    function LoadCharacterList() {
        CharacterAPI.getCharactersForUser($scope.userData.username)
            .then(function (characterList) {
                $scope.characterList = characterList;
                $scope.$apply();
            });
    }

    // Utility Functions
    function getSelectedCharacter() {

    }

    $scope.characterList.forEach(function (character, cIndex, arr) {
        arr[cIndex].selected = false;
    });
});