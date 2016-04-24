var app = angular.module("ttControllers");
app.controller('ttStudentsTabControllerForTeacher', [
    '$scope',
    'UserType',
    'UserStatus',
    'ngDialog',
    '$controller',
    function ($scope, UserType, UserStatus, ngDialog, $controller) {
        $controller('ttStudentsTabBaseController', { $scope: $scope });
        $controller('ttBaseInviteUserController', { $scope: $scope });

        $scope.userType = UserType.Teacher;

        $scope.gridOptions.columnDefs = [
              { name: window.resources.photoColumnName, field: 'Photo', enableSorting: false, width: 70, cellClass: 'ui-grid-hcenter', cellTemplate: 'photo-template' },
              { name: window.resources.uidColumnName, field: 'Uid', enableSorting: true, width: 100, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.fullnameColumnName, field: 'FullName', enableSorting: false, width: 170, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.actionsColumnName, enableSorting: false, cellTemplate: 'students-actions-template' }
        ];

        $scope.inviteDilaogController = 'ttInviteStudentDialogController';
        $scope.inviteDialogData = { title: window.resources.inviteStudentDilaogTitle };

        $scope.isViewGradesActionVisible = function (row) {
            return (row.entity.Status === UserStatus.Accepted);
        }

        $scope.isDeleteActionVisible = function (row) {
            return (row.entity.Status === UserStatus.Requested ||
                row.entity.Status === UserStatus.Accepted);
        }

        $scope.viewGrades = function (grid, row) {
            ngDialog.open({ template: '/Teacher/Group/ViewGradesDialog', data: { uid: row.entity.Uid, groupId: $scope.groupId } });
        }

        $scope.delete = function (grid, row) {
            var dialog = ngDialog.open({ template: '/Teacher/Group/DeleteUserDialog', controller: "ttDeleteStudentDialogController", data: { user: row.entity, groupId: $scope.groupId } });
            dialog.closePromise.then(function (response) {
                var result = response.value;
                if (typeof result !== "undefined" && result === true) {
                    $scope.reload();
                }
            });
        }

        $scope.initialize();
    }]);