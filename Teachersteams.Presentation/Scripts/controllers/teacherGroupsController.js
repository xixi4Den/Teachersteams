var app = angular.module("ttSinglePageApp");
app.controller('ttTeacherGroupsController', function ($scope) {
    $scope.groups = [{ id: 1, name: 'Test Group' }, { id: 2, name: 'Super group' }, { id: 3, name: 'English' },
        { id: 4, name: 'Math' }, { id: 5, name: 'Driving' }];
});