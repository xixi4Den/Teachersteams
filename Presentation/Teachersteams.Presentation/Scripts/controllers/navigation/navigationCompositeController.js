var app = angular.module("ttControllers");
app.controller('ttNavigationCompositeController', function ($scope, $location) {
    $scope.isCollapsed = true;
    $scope.$on('$routeChangeSuccess', function () {
        $scope.isCollapsed = true;
    });
});