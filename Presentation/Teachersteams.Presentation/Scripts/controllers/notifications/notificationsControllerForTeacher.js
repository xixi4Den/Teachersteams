var app = angular.module("ttControllers");
app.controller('ttNotificationsControllerForTeacher', [
    '$scope',
    '$controller',
    '$ttTeacherService',
    function ($scope, $controller, $ttTeacherService) {
        $controller('ttNotificationsBaseController', { $scope: $scope });

        $scope.messagePattern = window.resources.invitationMessageForTeacher;
        $scope.requestsFn = $ttTeacherService.requests;
        $scope.responseFn = $ttTeacherService.response;

        $scope.get();
    }]);