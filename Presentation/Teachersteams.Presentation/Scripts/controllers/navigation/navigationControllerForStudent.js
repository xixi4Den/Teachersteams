var app = angular.module("ttControllers");
app.controller('ttNavigationControllerForStudent', [
    '$scope',
    '$ttStudentService',
    '$controller',
    function ($scope, $ttStudentService, $controller) {
        $controller('ttUserSpecificNavigationBaseController', { $scope: $scope });

        $scope.requestsCountFn = $ttStudentService.requestsCount;

        $scope.refreshCount();
    }]);