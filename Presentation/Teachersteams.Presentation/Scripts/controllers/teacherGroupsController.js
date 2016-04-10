﻿var app = angular.module("ttControllers");
app.controller('ttTeacherGroupsController', ['$scope', 'ngDialog', function ($scope, ngDialog) {
    $scope.createGroup = function(e) {
        var dialog = ngDialog.open({ template: '/Teacher/Group/CreateDialog', controller: 'ttCreateGroupController' });
        dialog.closePromise.then(function (newGroup) {
            var result = newGroup.value;
            if ((typeof result !== "undefined") && result.hasOwnProperty('Id') && (typeof result.Id !== "undefined")) {
                $scope.$broadcast('create_group', result);
            }
        });
    }
}]);