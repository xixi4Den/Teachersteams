var app = angular.module("ttControllers");
app.controller('ttGroupListBaseController', ['$scope', '$ttGroupService', function ($scope, $ttGroupService) {
    $scope.maxCount = 5;
    $scope.index = 1;
    $scope.filterType = null;
    $scope.isEmpty = false;

    $scope.getGroups = function (index) {
        $ttGroupService.get($scope.filterType, index, $scope.maxCount)
            .then(function (response) {
                if (response.data.length > 0) {
                    $scope.groups = response.data;
                    $scope.index = index;
                    $scope.isEmpty = false;
                } else {
                    $scope.isEmpty = true;
                }
            });
    };

    $scope.next = function () {
        var nextIndex = $scope.index + 1;
        if ($scope.groups.length > 0) {
            $scope.getGroups(nextIndex);
        }
    }

    $scope.previous = function () {
        var previousIndex = $scope.index - 1;
        if (previousIndex >= 1) {
            $scope.getGroups(previousIndex);
        }
    }
}]);