var app = angular.module("ttControllers");
app.controller('ttGroupListControllerForTeacher', ['$scope', 'GroupFilterType', '$controller', function ($scope, GroupFilterType, $controller) {
    $controller('ttGroupListBaseController', { $scope: $scope });

    $scope.filterType = GroupFilterType.Own;

    $scope.$on("create_group", function (event, newGroup) {
        if ($scope.groups.length === $scope.maxCount) {
            $scope.groups.pop();
        }
        $scope.groups.unshift(newGroup);
    });

    $scope.getGroups($scope.index);
}]);