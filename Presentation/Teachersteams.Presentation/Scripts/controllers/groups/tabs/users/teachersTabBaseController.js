var app = angular.module("ttControllers");
app.controller('ttTeachersTabBaseController', [
    '$scope',
    '$ttTeacherService',
    '$controller',
    function ($scope, $ttTeacherService, $controller) {
        $controller('ttUsersTabBaseController', { $scope: $scope });

        
        $scope.gridItemsCountFn = $ttTeacherService.count;
        $scope.gridItemsGetFn = $ttTeacherService.get;
    }]);