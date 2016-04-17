var app = angular.module("ttControllers");
app.controller('ttGroupTeachersTabController', [
    '$scope',
    '$ttTeacherService',
    '$controller',
    function ($scope, $ttTeacherService, $controller) {
        $controller('ttGroupUsersTabBaseController', { $scope: $scope });

        $scope.inviteDilaogController = 'ttInviteTeacherDialogController';
        $scope.inviteDialogData = { title: window.resources.inviteTeacherDilaogTitle };
        $scope.gridOptions.columnDefs = [
              { name: window.resources.photoColumnName, field: 'Photo', enableSorting: false, width: 70, cellClass: 'ui-grid-hcenter', cellTemplate: 'photo-template' },
              { name: window.resources.uidColumnName, field: 'Uid', enableSorting: true, width: 100, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.fullnameColumnName, field: 'FullName', enableSorting: false, width: 170, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.actionsColumnName, enableSorting: false, cellTemplate: 'actions-template' }
        ];
        $scope.gridItemsCountFn = $ttTeacherService.count;
        $scope.gridItemsGetFn = $ttTeacherService.get;

        $scope.test = function (grid, row) {
            console.log(row);
        }

        $scope.initialize();
    }]);