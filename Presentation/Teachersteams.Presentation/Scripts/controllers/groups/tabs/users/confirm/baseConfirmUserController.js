var app = angular.module("ttControllers");
app.controller('ttBaseConfirmUserController', [
    '$scope',
    'UserStatus',
    '$stateParams',
    function ($scope, UserStatus, $stateParams) {
        $scope.checkRequest = function () {
            $scope.doesHaveRequestFn($stateParams.groupId).then(function (response) {
                $scope.doesHaveRequest = response.data;
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
                    $scope.doesHaveRequest = false;
                    $scope.reload();
                });
        }
    }]);