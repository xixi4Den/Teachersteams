var app = angular.module("ttControllers");
app.controller('ttNavigationControllerForTeacher', [
    '$scope',
    '$ttTeacherService',
    '$controller',
    function ($scope, $ttTeacherService, $controller) {
        $controller('ttUserSpecificNavigationBaseController', { $scope: $scope });

        $scope.requestsCountFn = $ttTeacherService.requestsCount;

        $scope.refreshCount();
    }]);