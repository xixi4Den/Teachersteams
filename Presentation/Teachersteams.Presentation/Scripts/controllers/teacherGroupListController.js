var app = angular.module("ttControllers");
app.controller('ttTeacherGroupListController', ['$scope', '$controller', function ($scope, $controller) {
    $controller('ttGroupListBaseController', { $scope: $scope });

    $scope.filterType = 1;

    $scope.$on("create_group", function (event, newGroup) {
        if ($scope.groups.length === $scope.maxCount) {
            $scope.groups.pop();
        }
        $scope.groups.unshift(newGroup);
    });

    $scope.getGroups($scope.index);
}]);