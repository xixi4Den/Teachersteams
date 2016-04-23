var app = angular.module("ttControllers");
app.controller('ttUserSpecificNavigationBaseController', [
    '$scope',
    function ($scope) {
        $scope.isNotificationsBudgeVisible = function () {
            return $scope.notificationsCount > 0;
        }

        $scope.refreshCount = function () {
            $scope.requestsCountFn().then(function (response) {
                $scope.notificationsCount = response.data;
            });
        }

        $scope.$on('$notificationsCountChanged', function () {
            $scope.refreshCount();
        });
    }]);