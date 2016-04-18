var app = angular.module("ttControllers");
app.controller('ttBaseTeachersTabController', [
    '$scope',
    '$ttTeacherService',
    '$controller',
    function ($scope, $ttTeacherService, $controller) {
        $controller('ttBaseUsersTabController', { $scope: $scope });

        
        $scope.gridItemsCountFn = $ttTeacherService.count;
        $scope.gridItemsGetFn = $ttTeacherService.get;
    }]);