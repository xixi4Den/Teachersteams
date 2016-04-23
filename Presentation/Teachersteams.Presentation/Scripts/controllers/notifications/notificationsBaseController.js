var app = angular.module("ttControllers");
app.controller('ttNotificationsBaseController', ['$scope', '$rootScope', 'UserStatus', function ($scope, $rootScope, UserStatus) {
    $scope.messagePattern = "";
    $scope.requestsFn = null;
    $scope.responseFn = null;

    $scope.get = function () {
        $scope.requestsFn()
            .then(function (response) {
                $scope.requests = response.data;
            });
    }

    $scope.accept = function (request) {
        request.Response = UserStatus.Accepted;
        $scope.response(request);
    }

    $scope.decline = function (request) {
        request.Response = UserStatus.Declined;
        $scope.response(request);
    }

    $scope.response = function (responseData) {
        $scope.responseFn(responseData)
            .then(function () {
                var responsedInvitation = _.findWhere($scope.requests, { GroupId: responseData.GroupId });
                $scope.requests = _.without($scope.requests, responsedInvitation);
                $rootScope.$broadcast('$notificationsCountChanged', null);
            });
    }
}]);