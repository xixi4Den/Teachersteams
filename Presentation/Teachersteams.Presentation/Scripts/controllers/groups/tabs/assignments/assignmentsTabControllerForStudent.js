var app = angular.module("ttControllers");
app.controller('ttAssignmentsTabControllerForStudent', [
    '$scope',
    'ngDialog',
    '$controller',
    function ($scope, ngDialog, $controller) {
        $controller('ttAssignmentsTabBaseController', { $scope: $scope });

        $scope.gridOptions.columnDefs = [
              { name: window.resources.assignmentsGridTitleColumnName, field: 'Title', enableSorting: true, width: 140, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.assignmentsGridExpirationColumnName, field: 'ExpirationDate', enableSorting: true, width: 120, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
              { name: window.resources.assignmentsGridStatusColumnName, field: 'StatusName', enableSorting: false, width: 70, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.actionsColumnName, enableSorting: false, cellTemplate: 'assignment-actions-template-for-student' }
        ];

        $scope.initializeGrid();

        $scope.isCompleteActionVisible = function(row) {
            return true;
        }

        $scope.complete = function(grid, row) {
            
        }

        $scope.isSeeResultActionVisible = function (row) {
            return true;
        }

        $scope.seeResult = function (grid, row) {

        }
    }]);