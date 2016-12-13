characterApp.controller('app-controller', function ($scope, $rootScope, UserAPI) {
    $scope.appStates = { create: 'create', select: 'select' };
    $scope.appState = $scope.appStates.select;

    $scope.userData = {
        username: null
    };

    UserAPI.getUserInformation()
    .then(function (userData) {
        $scope.userData.username = userData.username;
        $rootScope.$emit('UsernameLoaded');
    })
});