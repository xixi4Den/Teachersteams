var app = angular.module("ttControllers");
app.controller('ttStudentsTabControllerForTeacher', [
    '$scope',
    'UserType',
    '$controller',
    function ($scope, UserType, $controller) {
        $controller('ttStudentsTabBaseController', { $scope: $scope });
        $controller('ttBaseInviteUserController', { $scope: $scope });

        $scope.userType = UserType.Teacher;

        $scope.inviteDilaogController = 'ttInviteStudentDialogController';
        $scope.inviteDialogData = { title: window.resources.inviteStudentDilaogTitle };

        $scope.initialize();
    }]);