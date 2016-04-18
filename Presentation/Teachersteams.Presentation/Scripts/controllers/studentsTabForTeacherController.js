var app = angular.module("ttControllers");
app.controller('ttStudentsTabForTeacherController', [
    '$scope',
    '$controller',
    function ($scope, $controller) {
        $controller('ttBaseStudentsTabController', { $scope: $scope });
        $controller('ttBaseInviteUserController', { $scope: $scope });

        $scope.inviteDilaogController = 'ttInviteStudentDialogController';
        $scope.inviteDialogData = { title: window.resources.inviteStudentDilaogTitle };

        $scope.initialize();
    }]);