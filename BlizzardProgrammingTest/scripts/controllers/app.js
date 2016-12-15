/**
    Front-end parent controller. Has two child controllers:
    1) Character Select controller
    2) Character Creation controller

    Fetches uesrname from the back-end. Back-end gets the username through
    the user's windows auth-token.

    @namespace Application
*/
characterApp.controller('app-controller', function ($scope, $rootScope, UserAPI) {
    $scope.appStates = { create: 'create', select: 'select' };
    $scope.appState = $scope.appStates.select;

    // User's username + other assorted data (that we don't have/need right now)
    // Available by default to both child controllers through the $scope object.
    $scope.userData = {
        username: null
    };

    // Get the user's information from the back-end and then emit the signal
    // to the rest of the application. This emit signal will load the user's
    // character list and the race/class data for the create character page.
    UserAPI.getUserInformation()
        .then(function (userData) {
            $scope.userData.username = userData.username;
            $rootScope.$emit('UsernameLoaded');
        });
});