var app = angular.module("ttControllers");
app.controller('ttStudentsTabControllerForStudent', [
    '$scope',
    '$ttStudentService',
    'UserType',
    '$controller',
    function ($scope, $ttStudentService, UserType, $controller) {
        $controller('ttStudentsTabBaseController', { $scope: $scope });
        $controller('ttBaseConfirmUserController', { $scope: $scope });

        $scope.userType = UserType.Student;

        $scope.anyRequestFn = $ttStudentService.anyRequest;
        $scope.responseFn = $ttStudentService.response;
        $scope.checkRequest();

        $scope.initialize();
    }]);