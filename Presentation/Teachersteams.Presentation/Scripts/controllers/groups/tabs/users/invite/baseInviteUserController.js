var app = angular.module("ttControllers");
app.controller('ttBaseInviteUserController', [
    '$scope',
    'ngDialog',
    '$gridHelper',
    function ($scope, ngDialog, $gridHelper) {
        $scope.inviteDilaogController = null;
        $scope.inviteDialogData = null;

        $scope.invite = function (e) {
            var dialog = ngDialog.open({ template: '/Teacher/Group/InviteUserDialog', controller: $scope.inviteDilaogController, data: $scope.inviteDialogData });
            dialog.closePromise.then(function (newUser) {
                var result = newUser.value;
                if ((typeof result !== "undefined") && result.hasOwnProperty('Id') && (typeof result.Id !== "undefined")) {
                    $gridHelper.getPage();
                }
            });
        }
    }]);