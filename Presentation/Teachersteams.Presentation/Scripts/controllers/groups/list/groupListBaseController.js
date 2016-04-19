var app = angular.module("ttControllers");
app.controller('ttGroupListBaseController', ['$scope', '$ttGroupService', function ($scope, $ttGroupService) {
    $scope.maxCount = 5;
    $scope.index = 1;
    $scope.isEmpty = false;
    $scope.availableFilters = [];
    $scope.selectedFilter = {};

    $scope.getGroups = function (index) {
        $ttGroupService.get($scope.selectedFilter.Id, index, $scope.maxCount)
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

    $scope.applyFilter = function (id) {
        var filterId = typeof id !== 'undefined' ? id : $scope.availableFilters[0].Id;
        $scope.selectedFilter = _.findWhere($scope.availableFilters, { Id: filterId });
        $scope.getGroups(1);
    }

    $scope.isFilterVisible = function() {
        return $scope.availableFilters.length > 1;
    }
}]);