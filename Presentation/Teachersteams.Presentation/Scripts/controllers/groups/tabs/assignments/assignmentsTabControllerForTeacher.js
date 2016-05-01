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
                    $scope.$broadcast('create_group', result);
                }
            });
        }

        $scope.gridOptions.columnDefs = [
              { name: "Title", field: 'Title', enableSorting: true, width: 150, cellClass: 'ui-grid-vcenter' },
              { name: "Expiration", field: 'ExpirationDate', enableSorting: true, width: 130, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
              { name: "Status", field: 'StatusName', enableSorting: false, width: 80, cellClass: 'ui-grid-vcenter' },
              { name: "File", enableSorting: false, width: 90, cellClass: 'ui-grid-vcenter', cellTemplate: 'assignment-link-template'}
        ];

        $scope.initializeGrid();
    }]);