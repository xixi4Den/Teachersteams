var app = angular.module("ttControllers");
app.controller('ttBoardControllerForStudent', [
    '$scope',
    '$gridHelper',
    'uiGridConstants',
    '$ttBoardService',
    '$timeout',
    function ($scope, $gridHelper, uiGridConstants, $ttBoardService, $timeout) {

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
            { name: "Group", field: 'GroupTitle', enableSorting: false, width: 130, cellClass: 'ui-grid-vcenter' },
            { name: "Assignment", field: 'Title', enableSorting: true, width: 130, cellClass: 'ui-grid-vcenter' },
            { name: "Expiration", field: 'ExpirationDate', enableSorting: true, width: 100, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
            //{ name: "Completed", field: 'CompletionDate', enableSorting: false, width: 100, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
            { name: "Assignee", field: 'AssigneeName', enableSorting: false, width: 130, cellClass: 'ui-grid-vcenter' },
            { name: "Grade", field: 'Grade', enableSorting: false, width: 60, cellClass: 'ui-grid-vcenter ui-grid-hcenter' },
            //{ name: "Checked", field: 'CheckDate', enableSorting: false, width: 100, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
            { name: window.resources.actionsColumnName, enableSorting: false, width: 150 }
        ];

        $gridHelper.initialize($ttBoardService.countForStudent, $ttBoardService.getForStudent, [1], $scope.gridOptions);
        $gridHelper.getPage();

    }]);