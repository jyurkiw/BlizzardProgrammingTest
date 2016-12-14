characterApp.controller('character-create-controller', function ($scope, $rootScope, RaceAPI, CharacterAPI, DEFAULT_FACTION, DEFAULT_RACE, DEFAULT_CLASS, ALLIANCE, HORDE, CLASS_COLUMNS, MIN_CLASS_PER_COL) {
    $scope.allianceRaces = [];
    $scope.hordeRaces = [];
    $scope.wowClasses = [[], []];

    var raceClassData = null;

    $scope.newCharacterData = { name: null };
    $scope.error = null;
    
    $scope.currentFaction = DEFAULT_FACTION;

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

    $scope.classClickHandler = function (className) {
        $scope.wowClasses.forEach(function (classList) {
            classList.forEach(function (classData, idx, arr) {
                arr[idx].selected = className == arr[idx].name;
            });
        });
    }

    // Utility Functions
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

    // Needs lots of Testing
    function columnizeClasses(classListOrig) {
        // clone the list
        var classList = classListOrig.slice();

        if (classList.length > MIN_CLASS_PER_COL) {
            var classLists = [];
            var colSize = Math.ceil(classList.length / CLASS_COLUMNS);

            while (classList.length > 0) {
                classLists.push(objectizeRaceClassArray(classList.splice(0, colSize), DEFAULT_CLASS));
            }

            return classlists;
        } else {
            return [objectizeRaceClassArray(classList, DEFAULT_CLASS)];
        }
    }

    function objectizeRaceClassArray(arr, selectedValue) {
        return arr.map(function (val) {
            return { name: val, selected: val == selectedValue };
        })
    }

    $scope.createNewCharacterButtonClickHandler = function () {
        if ($scope.newCharacterData.name !== null) {
            $scope.error = null;

            var characterData = { 'name': $scope.newCharacterData.name, 'race': getCurrentRace(), 'class': getCurrentClass(), 'faction': $scope.currentFaction };
            CharacterAPI.addCharacterForUser(characterData, $scope.userData.username)
                .then(function (response) {
                    $rootScope.$emit('ReloadCharacterList', characterData);
                });

            $scope.raceClickHandler(DEFAULT_RACE, DEFAULT_FACTION);
            $scope.classClickHandler(DEFAULT_CLASS);
            $scope.newCharacterData = { name: null };

            $scope.$parent.appState = $scope.appStates.select;
        } else {
            $scope.error = "Character Name is Required.";
        }
    }

    $scope.cancelNewCharacterButtonClickHandler = function () {
        $scope.raceClickHandler(DEFAULT_RACE, DEFAULT_FACTION);
        $scope.classClickHandler(DEFAULT_CLASS);
        $scope.newCharacterData = { name: null };

        $scope.$parent.appState = $scope.appStates.select;
    }

    $rootScope.$on('UsernameLoaded', function () {
        RaceAPI.getRaceClassData($scope.userData.username)
            .then(function (data) {
                raceClassData = data;
                Object.keys(raceClassData).forEach(function (faction) {
                    Object.keys(raceClassData[faction]).forEach(function (race) {
                        raceClassData[faction][race] = columnizeClasses(raceClassData[faction][race]);
                    });
                });

                // Get alliance races
                $scope.allianceRaces = objectizeRaceClassArray(RaceAPI.getFactionRaces(raceClassData[ALLIANCE]), DEFAULT_RACE);
                $scope.hordeRaces = objectizeRaceClassArray(RaceAPI.getFactionRaces(raceClassData[HORDE]), DEFAULT_RACE);

                // Get default classes
                $scope.wowClasses = raceClassData[DEFAULT_FACTION][DEFAULT_RACE];
                $scope.$apply();
            });
    });
});