var app = angular.module("ttControllers");
app.controller('ttNotificationsControllerForTeacher', [
    '$scope',
    '$controller',
    '$ttTeacherService',
    function ($scope, $controller, $ttTeacherService) {
        $controller('ttNotificationsBaseController', { $scope: $scope });

        $scope.messagePattern = "You have been invited to teach in group";
        $scope.requestsFn = $ttTeacherService.requests;
        $scope.responseFn = $ttTeacherService.response;

        $scope.get();
    }]);