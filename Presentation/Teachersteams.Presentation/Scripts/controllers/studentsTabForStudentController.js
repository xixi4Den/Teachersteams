var app = angular.module("ttControllers");
app.controller('ttStudentsTabForStudentController', [
    '$scope',
    '$controller',
    function ($scope, $controller) {
        $controller('ttBaseStudentsTabController', { $scope: $scope });

        $scope.initialize();
    }]);