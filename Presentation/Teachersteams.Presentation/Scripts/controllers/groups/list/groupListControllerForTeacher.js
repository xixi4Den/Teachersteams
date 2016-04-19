var app = angular.module("ttControllers");
app.controller('ttGroupListControllerForTeacher', ['$scope', 'GroupFilterType', '$controller', function ($scope, GroupFilterType, $controller) {
    $controller('ttGroupListBaseController', { $scope: $scope });

    $scope.availableFilters = [
        { Id: GroupFilterType.Own, Text: window.resources.ownGroupFilterText },
        { Id: GroupFilterType.Assistant, Text: window.resources.assistantGroupFilterText },
        { Id: GroupFilterType.AllForTeacher, Text: window.resources.allGroupFilterText }];
    

    $scope.$on("create_group", function (event, newGroup) {
        if ($scope.groups.length === $scope.maxCount) {
            $scope.groups.pop();
        }
        $scope.groups.unshift(newGroup);
    });

    $scope.applyFilter();
}]);