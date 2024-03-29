﻿/**
    Character Select Controller.
    Handles character selection, character deletion, and transition to the Character Creation page.

    @namespace CharacterSelect
*/
characterApp.controller('character-select-controller', function ($scope, $rootScope, CharacterAPI) {
    $scope.characterList = [];

    $scope.undeleteMode = false;

    /**
        Character block decoration function.
        Generates a character block class based on character faction.

        @memberof CharacterSelect
        @param character Object A character object.
        @return string A CSS class to set the block background.
    */
    $scope.characterBlockClass = function (character) {
        return character.faction + '-block-background';
    };

    /**
        Character block Click Handler.
        Change the selected value of the character with the
        passed id to true, and all the others to false.

        @memberof CharacterSelect
        @param id Int The id of the character block.
    */
    $scope.characterBlockClickHandler = function (id) {
        $scope.characterList.forEach(function (character, cIndex, arr) {
            if (id === character.id) {
                arr[cIndex].selected = true;
            } else {
                arr[cIndex].selected = false;
            }
        });
    }

    /**
        Level One Hundred Coin Click Handler.
        Change the level of a character to 100.

        @memberof CharacterSelect
        @param id Int The id of the character.
    */
    $scope.oneHundredCoinClickHandler = function (id) {
        CharacterAPI.applyLevelToken(id)
            .then(function(results) {
                $rootScope.$emit('LevelTokenUsed');
                LoadCharacterList();
            });
    }

    /**
        New Character Button Click Handler.
        Set the app state to character create.

        @memberof CharacterSelect
    */
    $scope.newCharacterButtonClickHandler = function() {
        $scope.$parent.appState = $scope.appStates.create;
    }

    /**
        Delete Character Button Click Handler.
        Attempt to delete a character from the character list
        and reload the data from the server.

        @memberof CharacterSelect
    */
    $scope.deleteCharacterButtonClickHandler = function () {
        var target = getSelectedCharacter();

        if (target !== null) {
            $scope.characterList.splice($scope.characterList.map(function (e) { return e.id; }).indexOf(target.id), 1);
            CharacterAPI.deleteCharacterForUser(target.id)
                .then(function () {
                    $rootScope.$emit('LevelTokenUsed');
                    LoadCharacterList();
                });
        }
    }

    /**
        Undelete the selected character.

        @memberof CharacterSelect
    */
    $scope.undeleteCharacterButtonClickHandler = function () {
        var target = getSelectedCharacter();

        if (target !== null) {
            $scope.characterList.splice($scope.characterList.map(function (e) { return e.id; }).indexOf(target.id), 1);
            CharacterAPI.undeleteCharacterForUser(target.id)
                .then(function () {
                    $rootScope.$emit('LevelTokenUsed');
                    LoadCharacterList();
                });
        }
    }

    /**
        Undelete-Mode Character Button Click Handler.
        Set to undelete mode.

        @memberof CharacterSelect
    */
    $scope.undeleteModeCharacterButtonClickHandler = function () {
        $scope.undeleteMode = true;
        LoadCharacterList();
    }

    /**
        Cancel Undelete-Mode Button Click Handler.
        Turn off undelete mode.

        @memberof CharacterSelect
    */
    $scope.cancelUndeleteModeButtonClickHandler = function () {
        $scope.undeleteMode = false;
        LoadCharacterList();
    }

    /**
        Signal handler for Reload Character List.
        Reloads the character list when received.

        @memberof CharacterSelect
    */
    $rootScope.$on('ReloadCharacterList', function (event, data) {
        $scope.characterList.push(data);
        LoadCharacterList();
    });

    /**
        Signal handler for Username Loaded.
        Loads the character list when received.

        @memberof CharacterSelect
    */
    $rootScope.$on('UsernameLoaded', function () {
        LoadCharacterList();
    });

    /**
        Load the character list from the DB.
        When the character list is received,
        apply the change to the $scope so that
        any changes show up.

        @memberof CharacterSelect
    */
    function LoadCharacterList() {
        if (!$scope.undeleteMode) {
            CharacterAPI.getCharactersForUser($scope.userData.username)
                .then(function (characterList) {
                    $scope.characterList = characterList;
                    $scope.$apply();
                });
        } else {
            CharacterAPI.getDeletedCharactersForUser($scope.userData.username)
                .then(function (characterList) {
                    $scope.characterList = characterList;
                    $scope.$apply();
                });
        }
    }

    // Utility Functions
    /**
        Get the selected character.
        Selected character is not bound because it's marked by a style class
        and a boolean value in the objectized characterList structure.
        
        @memberof CharacterSelect
        @return int The character object from characterList. Null if no character is selected.
    */
    function getSelectedCharacter() {
        for (var i = 0; i < $scope.characterList.length; i++) {
            if ($scope.characterList[i].selected) {
                return $scope.characterList[i];
            }
        }

        return null;
    }

    /**
        Get the character with the passed id.

        @memberof CharacterSelect
        @param int id The id of the character.
        @return Object The character, or null if no character with that ID exists.
    */
    function getCharacterById(id) {
        var idx = $scope.characterList.map(function (e) { return e.id; }).indexOf(target.id);
        if (idx !== -1) {
            return $scope.characterList[idx];
        } else {
            return null;
        }
    }
});