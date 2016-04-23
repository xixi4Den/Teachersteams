var app = angular.module("ttControllers");
app.controller('ttDeleteTeacherDialogController', [
    '$scope',
    '$ttTeacherService',
    '$controller',
    function ($scope, $ttTeacherService, $controller) {
        $controller('ttDeleteUserDialogBaseController', { $scope: $scope });

        $scope.deleteFn = $ttTeacherService.delete;
    }]);