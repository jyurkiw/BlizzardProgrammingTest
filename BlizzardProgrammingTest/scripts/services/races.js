var raceClasses = angular.module('RaceServices', []);

raceClasses.factory('RaceAPI', ['$http', 'CLASS_COLUMNS', 'MIN_CLASS_PER_COL', function ($http) {
    // Query API
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

    function getFactionRaces(factionRaceClass) {
        return Object.keys(factionRaceClass);
    }

    return {
        getRaceClassData: getRaceClassData,
        getFactionRaces: getFactionRaces
    }
}]);