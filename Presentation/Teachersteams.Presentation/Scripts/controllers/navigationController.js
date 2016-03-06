var app = angular.module("ttSinglePageApp");
app.controller('ttNavigationController', function ($scope, $location) {
    $scope.isCollapsed = true;
    $scope.$on('$routeChangeSuccess', function () {
        $scope.isCollapsed = true;
    });
});