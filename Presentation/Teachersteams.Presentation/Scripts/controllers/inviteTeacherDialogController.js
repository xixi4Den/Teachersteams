var app = angular.module("ttControllers");
app.controller('ttInviteTeacherDialogController', ['$scope', '$vk', 'limitToFilter', function ($scope, $vk, limitToFilter) {
    $scope.result = null;

    $scope.getUsers = function(queryString) {
        return $vk.simpleCall('users.search', {
            q: queryString,
            fields: "uid, first_name, last_name, photo_medium"
        }).then(function (r) {
            var result = r.response.slice(1);
            return limitToFilter(result, 10);
        });
    }

    $scope.cancel = function (e) {
        $scope.closeThisDialog();
    }

    $scope.invite = function() {
        if ($scope.inviteTeacherForm.$valid) {
            
        }
    }
}]);