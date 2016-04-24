var app = angular.module("ttControllers");
app.controller('ttDeleteStudentDialogController', [
    '$scope',
    '$ttStudentService',
    '$controller',
    function ($scope, $ttStudentService, $controller) {
        $controller('ttDeleteUserDialogBaseController', { $scope: $scope });

        $scope.deleteFn = $ttStudentService.delete;
    }]);