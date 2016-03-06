var app = angular.module("ttSinglePageApp");
app.controller('ttMainController', ['$scope', '$vk', '$state', function ($scope, $vk, $state) {
    $scope.isTeacher = false;
    $scope.$watch('isTeacher', function (newValue, oldValue) {
        if (newValue) {
            $state.go('teacher/groups');
        } else {
            $state.go('student/groups');
        }
    });

    VK.init(function () {
        $vk.call('users.get', {})
            .then(function (r) {
                $scope.firstName = r.response[0].first_name;
                $scope.lastName = r.response[0].last_name;
            },
            function (r) {
                console.log("Error!!!");
            });
    });
}]);