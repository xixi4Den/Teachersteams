var app = angular.module("ttControllers");
app.controller('ttInviteTeacherDialogController', [
    '$scope',
    '$ttTeacherService',
    '$controller',
    function ($scope, $ttTeacherService, $controller) {
        $controller('ttInviteUserDialogBaseController', { $scope: $scope });

        $scope.inviteInternal = function (invitation) {
            return $ttTeacherService.invite(invitation);
        }
}]);