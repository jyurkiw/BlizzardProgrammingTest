characterApp.controller('character-create-controller', function ($scope, $rootScope) {
    $scope.allianceRaces = [];
    $scope.hordeRaces = [];
    $scope.wowClasses = [[], []];

    $scope.newCharacterData = { name: null };
    $scope.error = null;
    
    $scope.currentFaction = 'alliance';

    $scope.raceClickHandler = function (race, currentFaction) {
        $scope.currentFaction = currentFaction;

        [$scope.allianceRaces, $scope.hordeRaces].forEach(function (raceList) {
            raceList.forEach(function (raceData, idx, arr) {
                arr[idx].selected = race == arr[idx].name;
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

    $scope.createNewCharacterButtonClickHandler = function () {
        if ($scope.newCharacterData.name !== null) {
            $scope.error = null;

            var characterData = { 'name': $scope.newCharacterData.name, 'race': getCurrentRace(), 'class': getCurrentClass(), 'faction': $scope.currentFaction };

            $scope.raceClickHandler('Human', 'alliance');
            $scope.classClickHandler('Warrior');
            $scope.newCharacterData = { name: null };

            $rootScope.$emit('ReloadCharacterList', characterData);
            $scope.$parent.appState = $scope.appStates.select;
        } else {
            $scope.error = "Character Name is Required.";
        }
    }

    $scope.cancelNewCharacterButtonClickHandler = function () {
        $scope.raceClickHandler('Human', 'alliance');
        $scope.classClickHandler('Warrior');
        $scope.newCharacterData = { name: null };

        $scope.$parent.appState = $scope.appStates.select;
    }

    // Test Data
    // Alliance Races
    $scope.allianceRaces.push({ name: 'Human', selected: true });
    $scope.allianceRaces.push({ name: 'Gnome', selected: false });
    $scope.allianceRaces.push({ name: 'Worgen', selected: false });

    // Horde Races
    $scope.hordeRaces.push({ name: 'Orc', selected: false });
    $scope.hordeRaces.push({ name: 'Tauren', selected: false });
    $scope.hordeRaces.push({ name: 'Blood Elf', selected: false });

    // WoW Classes

    $scope.wowClasses[0].push({ name: 'Warrior', selected: true });
    $scope.wowClasses[0].push({ name: 'Druid', selected: false });
    $scope.wowClasses[1].push({ name: 'Death Knight', selected: false });
    $scope.wowClasses[1].push({ name: 'Mage', selected: false });
});