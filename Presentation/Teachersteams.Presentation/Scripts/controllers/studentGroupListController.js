var app = angular.module("ttControllers");
app.controller('ttStudentGroupListController', ['$scope', 'GroupFilterType', '$controller', function ($scope, GroupFilterType, $controller) {
    $controller('ttGroupListBaseController', { $scope: $scope });

    $scope.filterType = GroupFilterType.ForStudent;

    $scope.getGroups($scope.index);
}]);