var app = angular.module("ttSinglePageApp");
app.controller('ttTeacherGroupsController', function ($scope) {
    $scope.groups = ['Test Group', 'Super group', 'English', 'Math', 'Driving'];
});