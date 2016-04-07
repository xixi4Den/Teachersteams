var app = angular.module("ttControllers");
app.controller('ttGroupListController', ['$scope', '$ttGroupService', function ($scope, $ttGroupService) {
    $scope.maxCount = 5;
    $scope.index = 1;

    var getGroups = function (index) {
        $ttGroupService.get(1, index, $scope.maxCount)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.groups = response.data;
                    $scope.index = index;
                }
            });
    };

    getGroups(1);

    $scope.next = function () {
        var nextIndex = $scope.index + 1;
        if ($scope.groups.length > 0) {
            getGroups(nextIndex);
        }
    }

    $scope.previous = function () {
        var previousIndex = $scope.index - 1;
        if (previousIndex > 1) {
            getGroups(previousIndex);
        }
    }

    $scope.$on("create_group", function (event, newGroup) {
        if ($scope.groups.length === $scope.maxCount) {
            $scope.groups.pop();
        }
        $scope.groups.unshift(newGroup);
    });
}]);