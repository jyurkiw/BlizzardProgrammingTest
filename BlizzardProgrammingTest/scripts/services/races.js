/**
    Race Services deal with the Race/Class dataset.
*/
var raceClasses = angular.module('RaceServices', []);

/**
    The race API object interacts with the RaceClassController.

    @namespace RaceAPI
*/
raceClasses.factory('RaceAPI', ['$http', 'CLASS_COLUMNS', 'MIN_CLASS_PER_COL', function ($http) {
    // Query API

    /**
        Get race/class data to populate the create character UI.

        @memberof RaceAPI
        @param string username The name of the user.
        @return Promise The UI data response. Classes per race based on user filtering.
    */
    function getRaceClassData(username) {
        return new Promise(function (resolve, reject) {
            $http.get('http://localhost:53653/api/RaceClass/' + username)
                .then(function (response) {
                   resolve(response.data);
                })
                .catch(function (err) {
                    reject(err);
                });
        });
    }

    /**
        Get an array of factions from the race/class data.

        @memberof RaceAPI
        @param Object factionRaceClass Race/Class data form the RaceClass backend controller .
        @return Array A string array of factions.
    */
    function getFactionRaces(factionRaceClass) {
        return Object.keys(factionRaceClass);
    }

    return {
        getRaceClassData: getRaceClassData,
        getFactionRaces: getFactionRaces
    }
}]);