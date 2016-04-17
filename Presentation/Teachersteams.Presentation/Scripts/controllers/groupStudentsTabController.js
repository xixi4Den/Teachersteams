﻿var app = angular.module("ttControllers");
app.controller('ttGroupStudentsTabController', [
    '$scope',
    '$ttStudentService',
    '$controller',
    function ($scope, $ttStudentService, $controller) {
        $controller('ttGroupUsersTabBaseController', { $scope: $scope });

        $scope.inviteDilaogController = 'ttInviteStudentDialogController';
        $scope.inviteDialogData = { title: window.resources.inviteStudentDilaogTitle };

        $scope.gridOptions.columnDefs = [
              { name: window.resources.photoColumnName, field: 'Photo', enableSorting: false, width: 70, cellClass: 'ui-grid-hcenter', cellTemplate: 'photo-template' },
              { name: window.resources.uidColumnName, field: 'Uid', enableSorting: true, width: 100, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.fullnameColumnName, field: 'FullName', enableSorting: false, width: 170, cellClass: 'ui-grid-vcenter' }
        ];
        $scope.gridItemsCountFn = $ttStudentService.count;
        $scope.gridItemsGetFn = $ttStudentService.get;

        $scope.initialize();
    }]);