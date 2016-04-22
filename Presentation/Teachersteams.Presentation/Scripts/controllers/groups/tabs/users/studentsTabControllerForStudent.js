var app = angular.module("ttControllers");
app.controller('ttStudentsTabControllerForStudent', [
    '$scope',
    '$ttStudentService',
    '$controller',
    function ($scope, $ttStudentService, $controller) {
        $controller('ttStudentsTabBaseController', { $scope: $scope });
        $controller('ttBaseConfirmUserController', { $scope: $scope });

        $scope.doesHaveRequestFn = $ttStudentService.doesHaveRequest;
        $scope.responseFn = $ttStudentService.response;
        $scope.checkRequest();

        $scope.initialize();
    }]);