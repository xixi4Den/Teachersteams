var app = angular.module("ttControllers");
app.controller('ttNavigationController', function ($scope, $location) {
    $scope.isCollapsed = true;
    $scope.$on('$routeChangeSuccess', function () {
        $scope.isCollapsed = true;
    });
});