var app = angular.module("singlePageApp", ['ui.bootstrap', 'services']);
app.controller('MainController', ['$scope', '$vk', function ($scope, $vk) {
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