var app = angular.module("ttControllers");
app.controller('ttBoardControllerForStudent', [
    '$scope',
    '$gridHelper',
    'uiGridConstants',
    '$ttBoardService',
    'StudentBoardFilterType',
    'ngDialog',
    function ($scope, $gridHelper, uiGridConstants, $ttBoardService, StudentBoardFilterType, ngDialog) {
        $scope.availableFilters = [
            { Id: StudentBoardFilterType.New, Text: window.resources.studentDashboardFilterNameNew },
            { Id: StudentBoardFilterType.Expired, Text: window.resources.studentDashboardFilterNameExpired },
            { Id: StudentBoardFilterType.Completed, Text: window.resources.studentDashboardFilterNameCompleted },
            { Id: StudentBoardFilterType.Checked, Text: window.resources.studentDashboardFilterNameChecked }];

        $scope.selectedFilter = {
            id: $scope.availableFilters[0].Id
        }

        $scope.refresh = function() {
            $gridHelper.initialize($ttBoardService.countForStudent, $ttBoardService.getForStudent, [$scope.selectedFilter.id], $scope.gridOptions);
            $gridHelper.getPage();
        }

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
            gridElementId: 'boardGridForStudent'
        };

        $scope.gridOptions.columnDefs = [
            { name: window.resources.studentDashboardGridGroupColumnName, field: 'GroupTitle', enableSorting: false, width: 130, cellClass: 'ui-grid-vcenter' },
            { name: window.resources.studentDashboardGridAssignmentColumnName, field: 'Title', enableSorting: true, width: 130, cellClass: 'ui-grid-vcenter' },
            { name: window.resources.studentDashboardGridExpirationColumnName, field: 'ExpirationDate', enableSorting: true, width: 100, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
            { name: window.resources.studentDashboardGridAssigneeColumnName, field: 'AssigneeName', enableSorting: false, width: 130, cellClass: 'ui-grid-vcenter' },
            { name: window.resources.studentDashboardGridGradeColumnName, field: 'Grade', enableSorting: false, width: 60, cellClass: 'ui-grid-vcenter ui-grid-hcenter' },
            { name: window.resources.actionsColumnName, enableSorting: false, width: 150, cellTemplate: 'dashboard-grid-actions-template-for-student' }
        ];

        $scope.refresh();

        $scope.isDownloadResultActionVisible = function(row) {
            return typeof row.entity.AssignmentResultFileUrl !== 'undefined';
        }

        $scope.isCompleteActionVisible = function (row) {
            return $scope.selectedFilter.id === StudentBoardFilterType.New;
        }

        $scope.complete = function (grid, row) {
            var dialog = ngDialog.open({
                template: '/Student/Group/CompleteAssignmentDialog',
                controller: 'ttCompleteAssignmentDialogController',
                data: { id: row.entity.AssignmentId, title: row.entity.Title },
                closeByEscape: false,
                closeByNavigation: false,
                closeByDocument: false,
                showClose: false
            });
            dialog.closePromise.then(function (newAssignmentResult) {
                var result = newAssignmentResult.value;
                if ((typeof result !== "undefined") && result) {
                    $scope.refresh();
                }
            });
        }
    }]);