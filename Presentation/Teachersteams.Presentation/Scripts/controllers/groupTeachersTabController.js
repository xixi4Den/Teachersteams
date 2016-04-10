var app = angular.module("ttControllers");
app.controller('ttGroupTeachersTabController', ['$scope', 'ngDialog', function ($scope, ngDialog) {
    $scope.invite = function (e) {
        var dialog = ngDialog.open({ template: '/Teacher/Group/InviteTeacherDialog', controller: 'ttInviteTeacherDialogController' });
        dialog.closePromise.then(function (newGroup) {
            var result = newGroup.value;
            if ((typeof result !== "undefined") && result.hasOwnProperty('Id') && (typeof result.Id !== "undefined")) {
                $scope.$broadcast('create_group', result);
            }
        });
    }
}]);