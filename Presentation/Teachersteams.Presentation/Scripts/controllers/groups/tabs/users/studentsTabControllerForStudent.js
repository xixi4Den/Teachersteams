var app = angular.module("ttControllers");
app.controller('ttStudentsTabControllerForStudent', [
    '$scope',
    '$controller',
    function ($scope, $controller) {
        $controller('ttStudentsTabBaseController', { $scope: $scope });

        $scope.initialize();
    }]);