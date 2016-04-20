var app = angular.module("ttControllers");
app.controller('ttNotificationsBaseController', ['$scope', '$ttStudentService', function ($scope, $ttStudentService) {
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
        request.Response = 2;
        $scope.response(request);
    }

    $scope.decline = function (request) {
        request.Response = 3;
        $scope.response(request);
    }

    $scope.response = function (responseData) {
        $scope.responseFn(responseData)
            .then(function () {
                $scope.requests = _.without($scope.requests, _.findWhere($scope.requests, { GroupId: responseData.GroupId }));
            });
    }
}]);