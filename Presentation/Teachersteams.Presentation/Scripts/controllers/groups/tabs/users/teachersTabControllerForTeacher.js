var app = angular.module("ttControllers");
app.controller('ttTeachersTabControllerForTeacher', [
    '$scope',
    '$ttTeacherService',
    'UserStatus',
    'UserType',
    'ngDialog',
    '$controller',
    function ($scope, $ttTeacherService, UserStatus, UserType, ngDialog, $controller) {
        $controller('ttTeachersTabBaseController', { $scope: $scope });
        $controller('ttBaseInviteUserController', { $scope: $scope });
        $controller('ttBaseConfirmUserController', { $scope: $scope });

        $scope.userType = UserType.Teacher;

        $scope.inviteDilaogController = 'ttInviteTeacherDialogController';
        $scope.inviteDialogData = { title: window.resources.inviteTeacherDilaogTitle };

        $scope.gridOptions.columnDefs = [
              { name: window.resources.photoColumnName, field: 'Photo', enableSorting: false, width: 70, cellClass: 'ui-grid-hcenter', cellTemplate: 'photo-template' },
              { name: window.resources.uidColumnName, field: 'Uid', enableSorting: true, width: 100, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.fullnameColumnName, field: 'FullName', enableSorting: false, width: 170, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.actionsColumnName, enableSorting: false, cellTemplate: 'teachers-actions-template' }
        ];

        $scope.isEditPermissionsActionVisible = function(row) {
            return (row.entity.Status === UserStatus.Accepted);
        }

        $scope.isDeleteActionVisible = function (row) {
            return (row.entity.Status === UserStatus.Requested ||
                row.entity.Status === UserStatus.Accepted);
        }

        $scope.editPermissions = function (grid, row) {
            var dialog = ngDialog.open({ template: '/Teacher/Group/EditTeacherPermissionsDialog', data: { uid: row.entity.Uid, groupId: $scope.groupId } });
            dialog.closePromise.then(function (response) {
                var result = response.value;
                if (typeof result !== "undefined" && result === true) {
                    $scope.reload();
                }
            });
        }

        $scope.delete = function (grid, row) {
            var dialog = ngDialog.open({ template: '/Teacher/Group/DeleteUserDialog', controller: "ttDeleteTeacherDialogController", data: { user: row.entity, groupId: $scope.groupId } });
            dialog.closePromise.then(function (response) {
                var result = response.value;
                if (typeof result !== "undefined" && result === true) {
                    $scope.reload();
                }
            });
        }

        $scope.previousState = 'teacher/groups';
        $scope.anyRequestFn = $ttTeacherService.anyRequest;
        $scope.responseFn = $ttTeacherService.response;
        $scope.checkRequest();

        $scope.initialize();
    }]);