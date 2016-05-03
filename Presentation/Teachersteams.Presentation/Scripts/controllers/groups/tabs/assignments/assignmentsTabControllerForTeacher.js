var app = angular.module("ttControllers");
app.controller('ttAssignmentsTabControllerForTeacher', [
    '$scope',
    'ngDialog',
    '$controller',
    function ($scope, ngDialog, $controller) {
        $controller('ttAssignmentsTabBaseController', { $scope: $scope });

        $scope.create = function (e) {
            var dialog = ngDialog.open({ template: '/Teacher/Group/CreateAssignmentDialog', controller: 'ttCreateAssignmentsDialogController', closeByEscape: false, closeByNavigation: false, closeByDocument: false, showClose: false });
            dialog.closePromise.then(function (newAssignment) {
                var result = newAssignment.value;
                if ((typeof result !== "undefined") && result.hasOwnProperty('Id') && (typeof result.Id !== "undefined")) {
                    $scope.reloadGrid();
                }
            });
        }

        $scope.gridOptions.columnDefs = [
              { name: window.resources.assignmentsGridTitleColumnName, field: 'Title', enableSorting: true, width: 150, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.assignmentsGridExpirationColumnName, field: 'ExpirationDate', enableSorting: true, width: 130, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
              { name: window.resources.assignmentsGridStatusColumnName, field: 'StatusName', enableSorting: false, width: 80, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.actionsColumnName, enableSorting: false, cellTemplate: 'assignment-actions-template-for-teacher' }
        ];

        $scope.initializeGrid();

        $scope.isViewResultsActionVisible = function (row) {
            return true;
        }

        $scope.viewResults = function (grid, row) {

        }
    }]);