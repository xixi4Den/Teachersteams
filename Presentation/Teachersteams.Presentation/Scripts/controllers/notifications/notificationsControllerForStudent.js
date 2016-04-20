var app = angular.module("ttControllers");
app.controller('ttNotificationsControllerForStudent', [
    '$scope', 
    '$controller', 
    '$ttStudentService',
    function ($scope, $controller, $ttStudentService) {
        $controller('ttNotificationsBaseController', { $scope: $scope });

        $scope.messagePattern = "You have been invited to teach in group";
        $scope.requestsFn = $ttStudentService.requests;
        $scope.responseFn = $ttStudentService.response;

        $scope.get();
}]);