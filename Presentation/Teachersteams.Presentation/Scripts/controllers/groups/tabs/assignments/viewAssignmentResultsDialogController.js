var app = angular.module("ttControllers");
app.controller('ttViewAssignmentResultsDialogController', [
    '$scope',
    '$ttAssignmentService',
    '$gridHelper',
    'uiGridConstants',
    function ($scope, $ttAssignmentService, $gridHelper, uiGridConstants) {
        $scope.gridOptions = {
            rowHeight: 50,
            paginationPageSizes: [10],
            paginationPageSize: 10,
            useExternalPagination: true,
            useExternalSorting: true,
            enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
            enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                $scope.gridApi.core.on.sortChanged($scope, $gridHelper.sortChanged);
                $scope.gridApi.pagination.on.paginationChanged($scope, $gridHelper.paginationChanged);
            },
            gridElementId: 'assignmentResultsGrid'
        };

        $scope.gridOptions.columnDefs = [
              { name: window.resources.assignmentResultsGridPhotoColumnName, enableSorting: false, width: 70, cellClass: 'ui-grid-hcenter', cellTemplate: 'photo-template' },
              { name: window.resources.assignmentResultsGridStudentColumnName, field: 'StudentName', enableSorting: false, width: 130, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.assignmentResultsGridFinishedColumnName, field: 'CompletionDate', enableSorting: true, width: 100, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
              { name: window.resources.assignmentResultsGridAssigneeColumnName, field: 'AssigneeName', enableSorting: false, width: 130, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.assignmentResultsGridGradeColumnName, field: 'Grade', enableSorting: true, width: 60, cellClass: 'ui-grid-vcenter' },
              { name: window.resources.assignmentResultsGridCheckedColumnName, field: 'CheckDate', enableSorting: true, width: 100, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
              { name: window.resources.actionsColumnName, enableSorting: false }
        ];

        $gridHelper.initialize($ttAssignmentService.resultsCount, $ttAssignmentService.getResults, [$scope.ngDialogData.assignmentId], $scope.gridOptions);
        $gridHelper.getPage();

        $scope.cancel = function (e) {
            $scope.closeThisDialog();
        }
    }]);