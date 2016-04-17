var app = angular.module("ttControllers");
app.controller('ttStudentGroupListController', ['$scope', '$controller', function ($scope, $controller) {
    $controller('ttGroupListBaseController', { $scope: $scope });

    $scope.filterType = 3;

    $scope.getGroups($scope.index);
}]);