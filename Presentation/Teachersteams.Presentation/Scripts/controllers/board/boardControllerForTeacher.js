var app = angular.module("ttControllers");
app.controller('ttBoardControllerForTeacher', [
    '$scope',
    '$gridHelper',
    'uiGridConstants',
    '$ttBoardService',
    'TeacherBoardAssignFilterType',
    'TeacherBoardCheckFilterType',
    function ($scope, $gridHelper, uiGridConstants, $ttBoardService, TeacherBoardAssignFilterType, TeacherBoardCheckFilterType) {
        $scope.availableCheckFilters = [
            { Id: TeacherBoardCheckFilterType.Unchecked, Text: window.resources.teacherDashboardCheckFilterNameUnchecked },
            { Id: TeacherBoardCheckFilterType.Checked, Text: window.resources.teacherDashboardCheckFilterNameChecked }];

        $scope.availableAssignFilters = [
            { Id: TeacherBoardAssignFilterType.NotAssigned, Text: window.resources.teacherDashboardAssignFilterNameNotAssigned },
            { Id: TeacherBoardAssignFilterType.AssignedToMe, Text: window.resources.teacherDashboardAssignFilterNameAssignedToMe },
            { Id: TeacherBoardAssignFilterType.AssignedToOthers, Text: window.resources.teacherDashboardAssignFilterNameAssignedToOthers }];

        $scope.selectedFilters = {
            check: $scope.availableCheckFilters[0].Id,
            assign: $scope.availableAssignFilters[0].Id
        }

        $scope.getCheckFilter = function()
        {
            if ($scope.selectedFilters.assign === TeacherBoardAssignFilterType.NotAssigned) {
                return null;
            }
            return $scope.selectedFilters.check;
        }

        $scope.isCheckFilterSectionVisible = function() {
            return $scope.selectedFilters.assign !== TeacherBoardAssignFilterType.NotAssigned;
        }

        $scope.refresh = function () {
            $gridHelper.initialize($ttBoardService.countForTeacher, $ttBoardService.getForTeacher, [$scope.getCheckFilter(), $scope.selectedFilters.assign], $scope.gridOptions);
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
            gridElementId: 'boardGridForTeacher'
        };

        $scope.gridOptions.columnDefs = [
            { name: window.resources.teacherDashboardGridGroupColumnName, field: 'GroupTitle', enableSorting: false, width: 130, cellClass: 'ui-grid-vcenter' },
            { name: window.resources.teacherDashboardGridAssignmentColumnName, field: 'Title', enableSorting: true, width: 120, cellClass: 'ui-grid-vcenter' },
            { name: window.resources.teacherDashboardGridStudentColumnName, field: 'StudentName', enableSorting: true, width: 120, cellClass: 'ui-grid-vcenter' },
            { name: window.resources.teacherDashboardGridAssigneeColumnName, field: 'AssigneeName', enableSorting: false, width: 120, cellClass: 'ui-grid-vcenter' },
            { name: window.resources.teacherDashboardGridGradeColumnName, field: 'Grade', enableSorting: false, width: 60, cellClass: 'ui-grid-vcenter ui-grid-hcenter' },
            { name: window.resources.actionsColumnName, enableSorting: false, width: 150 }
        ];

        $scope.refresh();
}]);