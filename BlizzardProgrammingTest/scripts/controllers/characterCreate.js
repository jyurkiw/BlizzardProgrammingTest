﻿/**
    Collection of utility functions whose functionalify has been pieced-out into a separate library
    object to assist in testing.

    @param int MIN_CLASS_PER_COL Minimum classes per column.
    @param int CLASS_COLUMNS Number of columns.
    @param int DEFAULT_CLASS Default selected class.
*/
var CharacterCreateUtilities = function (MIN_CLASS_PER_COL, CLASS_COLUMNS, DEFAULT_CLASS) {
    /**
        Step through a passed class array and replce the base string values
        with JS objects that track selection and names, and set a specific
        selected value.

        The expected array format is [ string, string, string, ...etc ]
        The output format will be [ { name: string, selected: boolean }, ...etc ]

        Any array value equal to selectedValue will have their selected member set to true.
        Otherwise selected will be set to false.

        @memberof CharacterCreation
        @param arr Array The array to objectize.
        @param selectedValue The value to set selected to true.
        @return The objectized Race/Class array.
    */
    function objectizeRaceClassArray(arr, selectedValue) {
        return arr.map(function (val) {
            return { name: val, selected: val == selectedValue };
        })
    }

    // Needs lots of Testing
    /**
        Columnize the Class lists for each race into their column lists.
        This is needed for the class blocks to render in an N column format.
        The minimum items per column, and the number of columns are controlled
        with the MIN_CLASS_PER_COL and CLASS_COLUMNS constants.

        @memberof CharacterCreation
        @param classListOrig Array The class list to columnize.
        @return Array-of-Arrays The class list divided into CLASS_COLUMNS sublists of at least MIN_CLASS_PER_COL size.
    */
    function columnizeClasses(classListOrig) {
        // clone the list
        var classList = classListOrig.slice();

        if (classList.length > MIN_CLASS_PER_COL) {
            var classLists = [];

            if (classList.length <= MIN_CLASS_PER_COL * CLASS_COLUMNS) {
                while (classList.length > 0) {
                    classLists.push(objectizeRaceClassArray(classList.splice(0, MIN_CLASS_PER_COL), DEFAULT_CLASS));
                }
            } else {
                var colSize = Math.ceil(classList.length / CLASS_COLUMNS);
                var remainincCols = CLASS_COLUMNS;
                var colSizeReduced = false;

                while (classList.length > 0) {
                    classLists.push(objectizeRaceClassArray(classList.splice(0, colSize), DEFAULT_CLASS));

                    if (classList.length % --remainincCols == 0 && !colSizeReduced) {
                        colSize--;
                        colSizeReduced = true;
                    }
                }
            }

            return classLists;
        } else {
            return [objectizeRaceClassArray(classList, DEFAULT_CLASS)];
        }
    }

    return {
        objectizeRaceClassArray: objectizeRaceClassArray,
        columnizeClasses: columnizeClasses
    };
}

/*
    Expose for mocha testing.
    typeof is used instead of simply testing against undefined becuase
    the undefined macro is native to node and not the browser.
*/

if (typeof (module) !== 'undefined' && typeof (module.exports) !== 'undefined') {
    module.exports = CharacterCreateUtilities;
}

/**
    Character Creation Controller.
    Handles Faction selection, Race selection, class selection, and character name.

    @namespace CharacterCreation
*/
characterApp.controller('character-create-controller', function ($scope, $rootScope, RaceAPI, CharacterAPI, DEFAULT_FACTION, DEFAULT_RACE, DEFAULT_CLASS, ALLIANCE, HORDE, CLASS_COLUMNS, MIN_CLASS_PER_COL, DEATHKNIGHT_CLASS, DEATHKNIGHT_START_LEVEL) {
    // Alliance races
    $scope.allianceRaces = [];

    // Horde races
    $scope.hordeRaces = [];

    // Classes for selected race
    $scope.wowClasses = [[], []];

    // Race/Class data storage. Is not in $scope because it doesn't need to be.
    var raceClassData = null;

    $scope.newCharacterData = { name: null };
    $scope.error = null;
    
    $scope.currentFaction = DEFAULT_FACTION;

    var util = CharacterCreateUtilities(MIN_CLASS_PER_COL, CLASS_COLUMNS, DEFAULT_CLASS);

    // Utility Functions
    /**
        Return the currently selected class.

        @memberof CharacterCreation
    */
    function getCurrentClass() {
        var className = null;

        $scope.wowClasses.forEach(function (classList) {
            classList.forEach(function (classData) {
                if (classData.selected) {
                    className = classData.name;
                }
            });
        });

        return className;
    }

    /**
        Return the currently selected race.

        @memberof CharacterCreation
    */
    function getCurrentRace() {
        var raceName = null;

        [$scope.allianceRaces, $scope.hordeRaces].forEach(function (raceList) {
            raceList.forEach(function (raceData, idx, arr) {
                if (raceData.selected) {
                    raceName = raceData.name;
                }
            });
        });

        return raceName;
    }

    /**
        Race click handler.
        Sets character faction (based on race), character race, and class list (based on race and username).
        Sets all selected members in both race lists to false except for the race that was selected.

        @memberof CharacterCreation
        @param race string Character race
        @param currentFaction string The race's faction
    */
    $scope.raceClickHandler = function (race, currentFaction) {
        $scope.currentFaction = currentFaction;

        [$scope.allianceRaces, $scope.hordeRaces].forEach(function (raceList) {
            raceList.forEach(function (raceData, idx, arr) {
                arr[idx].selected = race == arr[idx].name;

                if (race == arr[idx].name) {
                    $scope.wowClasses = raceClassData[currentFaction][race];
                }
            });
        });
    }

    /**
        Class click handler.
        Sets character class selected member to true. Sets all others to false.
        Class will be set to true for both factions if available.

        @memberof CharacterCreation
        @param className string The name of the class.
    */
    $scope.classClickHandler = function (className) {
        $scope.wowClasses.forEach(function (classList) {
            classList.forEach(function (classData, idx, arr) {
                arr[idx].selected = className == arr[idx].name;
            });
        });
    }

    /**
        Create New Character Button Click Handler.
        Validate form data and create a new character.
        Reset the character creation UI.
        Send user back to character select.

        @memberof CharacterCreation
    */
    $scope.createNewCharacterButtonClickHandler = function () {
        // Validate character data.
        // It's possible to get a null back from getCurrentClass if you switch from one race to another
        // where the new race doesn't have access to the old class.
        if ($scope.newCharacterData.name !== null && getCurrentClass() != null) {
            $scope.error = null;

            // Build character payload
            var characterData = { 'name': $scope.newCharacterData.name, 'race': getCurrentRace(), 'class': getCurrentClass(), 'faction': $scope.currentFaction, 'level': 1 };
            if (characterData.race == DEATHKNIGHT_CLASS) {
                characterData.level = DEATHKNIGHT_START_LEVEL;
            }

            // Make a call to the create character API, wait for the response, and refresh the character data if necessary.
            CharacterAPI.addCharacterForUser(characterData, $scope.userData.username)
                .then(function (response) {
                    $rootScope.$emit('ReloadCharacterList', characterData);
                });

            $scope.raceClickHandler(DEFAULT_RACE, DEFAULT_FACTION);
            $scope.classClickHandler(DEFAULT_CLASS);
            $scope.newCharacterData = { name: null };

            $scope.$parent.appState = $scope.appStates.select;
        } else {
            $scope.error = "Character Name and Class is Required.";
        }
    }

    /**
        Cancel New Character Button Click Handler
        Reset Character Creation UI and send user back to character select.

        @memberof CharacterCreation
    */
    $scope.cancelNewCharacterButtonClickHandler = function () {
        $scope.raceClickHandler(DEFAULT_RACE, DEFAULT_FACTION);
        $scope.classClickHandler(DEFAULT_CLASS);
        $scope.newCharacterData = { name: null };

        $scope.$parent.appState = $scope.appStates.select;
    }

    /**
        Username Loaded signal handler.
        When the username is received from the server, retrieve and
        load the race/class data for the character creation form.
    */
    $rootScope.$on('UsernameLoaded', function () {
        LoadRaceClassData();
    });
    
    /**
        Level Token signal handler.
        Whena level token is used, reload class and race data from the backend.
        We have to do this because it might enable Death Knights.
    */
    $rootScope.$on('LevelTokenUsed', function () {
        LoadRaceClassData();
    });

    /**
        Load Race and Class data from the DB.

        @memberof CharacterCreation
    */
    function LoadRaceClassData() {
        RaceAPI.getRaceClassData($scope.userData.username)
            .then(function (data) {
                raceClassData = data;
                Object.keys(raceClassData).forEach(function (faction) {
                    Object.keys(raceClassData[faction]).forEach(function (race) {
                        raceClassData[faction][race] = util.columnizeClasses(raceClassData[faction][race]);
                    });
                });

                // Get alliance races
                $scope.allianceRaces = util.objectizeRaceClassArray(RaceAPI.getFactionRaces(raceClassData[ALLIANCE]), DEFAULT_RACE);
                $scope.hordeRaces = util.objectizeRaceClassArray(RaceAPI.getFactionRaces(raceClassData[HORDE]), DEFAULT_RACE);

                // Get default classes
                $scope.wowClasses = raceClassData[DEFAULT_FACTION][DEFAULT_RACE];
                $scope.$apply();
            });
    }
});