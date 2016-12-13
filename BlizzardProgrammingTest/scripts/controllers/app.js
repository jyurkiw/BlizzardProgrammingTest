characterApp.controller('app-controller', function ($scope) {
    $scope.appStates = { create: 'create', select: 'select' };
    $scope.appState = $scope.appStates.select;
});