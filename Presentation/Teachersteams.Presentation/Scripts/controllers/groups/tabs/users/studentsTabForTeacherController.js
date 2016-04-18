var app = angular.module("ttControllers");
app.controller('ttStudentsTabControllerForTeacher', [
    '$scope',
    '$controller',
    function ($scope, $controller) {
        $controller('ttStudentsTabBaseController', { $scope: $scope });
        $controller('ttBaseInviteUserController', { $scope: $scope });

        $scope.inviteDilaogController = 'ttInviteStudentDialogController';
        $scope.inviteDialogData = { title: window.resources.inviteStudentDilaogTitle };

        $scope.initialize();
    }]);