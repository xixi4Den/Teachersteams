var app = angular.module("ttControllers");
app.controller('ttInviteStudentDialogController', [
    '$scope',
    '$ttStudentService',
    '$controller',
    function ($scope, $ttStudentService, $controller) {
        $controller('ttInviteUserDialogBaseController', { $scope: $scope });

        $scope.inviteInternal = function (invitation) {
            return $ttStudentService.invite(invitation);
        }
    }]);