var app = angular.module("ttControllers");
app.controller('ttMainController', ['$scope', '$vk', '$state', '$http', 'AppContext', function ($scope, $vk, $state, $http, AppContext) {
    $scope.isTeacher = false;
    $scope.$watch('isTeacher', function (newValue, oldValue) {
        if (newValue) {
            $state.go('teacher/groups');
        } else {
            $state.go('student/groups');
        }
    });

    $http.get('/Home/Settings').success(function (response, status) {
        AppContext.apiUrl = response.BusinessApiUrl;
    });

    $vk.init(function () {
        $vk.call('users.get', {})
            .then(function (r) {
                AppContext.firstName = r.response[0].first_name;
                AppContext.lastName = r.response[0].last_name;
                AppContext.uid = r.response[0].uid;
                },
            function (r) {
                console.log("Error!!!");
            });
    });

    $scope.isLoading = false;
}]);