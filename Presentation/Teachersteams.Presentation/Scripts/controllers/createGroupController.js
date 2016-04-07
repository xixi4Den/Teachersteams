var app = angular.module("ttControllers");
app.controller('ttCreateGroupController', ['$scope', '$ttGroupService', 'ngToast', function ($scope, $ttGroupService, ngToast) {
    //var deferred = $q.defer();

    $scope.create = function (e) {
        if ($scope.addGroupForm.$valid) {
            $ttGroupService.create($scope.data)
                .then(function (response) {
                    ngToast.success("New group has been added");
                    $scope.closeThisDialog(response.data);
                });
        }
    }

    $scope.cancel = function (e) {
        $scope.closeThisDialog();
    }
}]);