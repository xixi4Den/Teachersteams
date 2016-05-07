var app = angular.module("ttControllers");
app.controller('ttViewAssignmentResultsDialogController', [
    '$scope',
    '$ttAssignmentService',
    '$gridHelper',
    'uiGridConstants',
    'AppContext',
    'ngDialog',
    function ($scope, $ttAssignmentService, $gridHelper, uiGridConstants, AppContext, ngDialog) {
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
              { name: window.resources.assignmentResultsGridGradeColumnName, field: 'Grade', enableSorting: true, width: 60, cellClass: 'ui-grid-vcenter ui-grid-hcenter' },
              { name: window.resources.assignmentResultsGridCheckedColumnName, field: 'CheckDate', enableSorting: true, width: 100, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
              { name: window.resources.actionsColumnName, enableSorting: false, cellTemplate: 'assignment-results-actions-template' }
        ];

        $gridHelper.initialize($ttAssignmentService.resultsCount, $ttAssignmentService.getResults, [$scope.ngDialogData.assignmentId], $scope.gridOptions);
        $gridHelper.getPage();

        $scope.isAssignToMeItemVisible = function (row) {
            return !isAssigned(row);
        }

        $scope.isDownloadAssignmentResultItemVisible = function (row) {
            return isAssignedToMe(row);
        }

        $scope.isRateAssignmentResultItemVisible = function (row) {
            return isAssignedToMe(row) && !isRated(row);
        }

        $scope.assignToMe = function (grid, row) {
            $ttAssignmentService.assignResult(row.entity.Id).then(function() {
                $gridHelper.getPage();
            });
        }

        $scope.rate = function (grid, row) {
            var dialog = ngDialog.open({
                template: '/Teacher/Group/GradeAssignmentResultDialog',
                controller: 'ttGradeAssignmentResultDialogController',
                data: { id: row.entity.Id, student: row.entity.StudentName }
            });
            dialog.closePromise.then(function (result) {
                var grade = result.value;
                if ((typeof grade !== "undefined")) {
                    $gridHelper.getPage();
                }
            });
        }

        $scope.cancel = function (e) {
            $scope.closeThisDialog();
        }

        function isAssigned(row) {
            return row.entity.AssigneeTeacherUid;
        }

        function isAssignedToMe(row) {
            return row.entity.AssigneeTeacherUid === AppContext.uid.toString();
        }

        function isRated(row) {
            return typeof row.entity.Grade !== "undefined" && row.entity.Grade !== null;
        }
    }]);