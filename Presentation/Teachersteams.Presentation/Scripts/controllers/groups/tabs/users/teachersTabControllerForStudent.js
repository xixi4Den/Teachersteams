﻿var app = angular.module("ttControllers");
app.controller('ttTeachersTabControllerForStudent', [
    '$scope',
    'UserType',
    '$controller',
    function ($scope, UserType, $controller) {
        $controller('ttTeachersTabBaseController', { $scope: $scope });

        $scope.userType = UserType.Student;

        $scope.gridOptions.columnDefs = [
              { name: window.resources.photoColumnName, field: 'Photo', enableSorting: false, width: 70, cellClass: 'ui-grid-hcenter', cellTemplate: 'photo-template' },
              { name: window.resources.uidColumnName, field: 'Uid', enableSorting: true, width: 100, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.fullnameColumnName, field: 'FullName', enableSorting: false, width: 170, cellClass: 'ui-grid-vcenter' },
        ];

        $scope.initialize();
    }]);