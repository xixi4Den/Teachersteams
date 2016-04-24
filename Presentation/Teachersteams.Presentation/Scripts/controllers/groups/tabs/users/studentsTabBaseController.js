var app = angular.module("ttControllers");
app.controller('ttStudentsTabBaseController', [
    '$scope',
    '$ttStudentService',
    '$controller',
    function ($scope, $ttStudentService, $controller) {
        $controller('ttUsersTabBaseController', { $scope: $scope });

        $scope.gridItemsCountFn = $ttStudentService.count;
        $scope.gridItemsGetFn = $ttStudentService.get;
    }]);