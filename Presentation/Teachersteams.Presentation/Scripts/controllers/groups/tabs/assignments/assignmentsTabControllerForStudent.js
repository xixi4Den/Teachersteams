var app = angular.module("ttControllers");
app.controller('ttAssignmentsTabControllerForStudent', [
    '$scope',
    'ngDialog',
    '$controller',
    'UserType',
    function ($scope, ngDialog, $controller, UserType) {
        $controller('ttAssignmentsTabBaseController', { $scope: $scope });

        $scope.userType = UserType.Student;

        $scope.gridOptions.columnDefs = [
              { name: window.resources.assignmentsGridTitleColumnName, field: 'Title', enableSorting: true, width: 140, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.assignmentsGridExpirationColumnName, field: 'ExpirationDate', enableSorting: true, width: 120, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
              { name: window.resources.assignmentsGridStatusColumnName, field: 'StatusName', enableSorting: false, width: 70, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.actionsColumnName, enableSorting: false, cellTemplate: 'assignment-actions-template-for-student' }
        ];

        $scope.initializeGrid();

        $scope.isCompleteActionVisible = function(row) {
            return !row.entity.IsCompleted;
        }

        $scope.complete = function(grid, row) {
            var dialog = ngDialog.open({
                template: '/Student/Group/CompleteAssignmentDialog',
                controller: 'ttCompleteAssignmentDialogController',
                data: { id: row.entity.Id, title: row.entity.Title },
                closeByEscape: false,
                closeByNavigation: false,
                closeByDocument: false,
                showClose: false
            });
            dialog.closePromise.then(function (newAssignmentResult) {
                var result = newAssignmentResult.value;
                if ((typeof result !== "undefined") && result) {
                    $scope.reloadGrid();
                }
            });
        }

        $scope.isViewResultActionVisible = function (row) {
            return row.entity.IsCompleted;
        }

        $scope.viewResult = function (grid, row) {
            ngDialog.open({
                template: '/Student/Group/ViewAssignmentResultDialog',
                controller: 'ttViewAssignmentResultDialogController',
                data: { id: row.entity.Id, assignmentTitle: row.entity.Title }
            });
        }
    }]);