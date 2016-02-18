var app = angular.module("ttSinglePageApp", ['ui.bootstrap', 'ttServices', 'NgSwitchery']);
app.controller('ttMainController', ['$scope', '$vk', function ($scope, $vk) {
    $scope.isTeacher = true;

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