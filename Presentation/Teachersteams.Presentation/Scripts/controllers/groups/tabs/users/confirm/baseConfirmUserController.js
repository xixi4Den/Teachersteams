var app = angular.module("ttControllers");
app.controller('ttBaseConfirmUserController', [
    '$scope',
    '$rootScope',
    'UserStatus',
    '$stateParams',
    '$state',
    function ($scope, $rootScope, UserStatus, $stateParams, $state) {
        $scope.checkRequest = function () {
            $scope.anyRequestFn($stateParams.groupId).then(function (response) {
                $scope.anyRequest = response.data;
        });}

        $scope.accept = function () {
            var request = {
                GroupId: $stateParams.groupId,
                Response: UserStatus.Accepted
            }
            $scope.response(request);
        }

        $scope.decline = function () {
            var request = {
                GroupId: $stateParams.groupId,
                Response: UserStatus.Declined
            }
            $scope.response(request);
        }

        $scope.response = function (responseData) {
            $scope.responseFn(responseData)
                .then(function () {
                    $scope.anyRequest = false;
                    $rootScope.$broadcast('$notificationsCountChanged', null);
                    $state.go($scope.previousState);
                    $rootScope.$broadcast('$groupListChanged', null);
                });
        }
    }]);