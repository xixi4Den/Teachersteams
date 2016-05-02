var app = angular.module("ttControllers");
app.controller('ttAssignmentsTabControllerForStudent', [
    '$scope',
    'ngDialog',
    '$controller',
    function ($scope, ngDialog, $controller) {
        $controller('ttAssignmentsTabBaseController', { $scope: $scope });

        $scope.gridOptions.columnDefs = [
              { name: window.resources.assignmentsGridTitleColumnName, field: 'Title', enableSorting: true, width: 150, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.assignmentsGridExpirationColumnName, field: 'ExpirationDate', enableSorting: true, width: 130, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
              { name: window.resources.assignmentsGridStatusColumnName, field: 'StatusName', enableSorting: false, width: 80, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.assignmentsGridFileColumnName, enableSorting: false, width: 90, cellClass: 'ui-grid-vcenter', cellTemplate: 'assignment-link-template' }
        ];

        $scope.initializeGrid();
    }]);