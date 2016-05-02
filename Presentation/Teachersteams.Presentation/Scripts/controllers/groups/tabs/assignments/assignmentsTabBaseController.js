var app = angular.module("ttControllers");
app.controller('ttAssignmentsTabBaseController', [
    '$scope',
    '$gridHelper',
    'uiGridConstants',
    '$stateParams',
    '$ttAssignmentService',
    function ($scope, $gridHelper, uiGridConstants, $stateParams, $ttAssignmentService) {
        $scope.gridItemsCountFn = null;
        $scope.gridItemsGetFn = null;

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
            }
        };

        $scope.initializeGrid = function () {
            $gridHelper.initialize($ttAssignmentService.count, $ttAssignmentService.get, [$stateParams.groupId], $scope.gridOptions);
            $gridHelper.getPage();
        }

        $scope.reloadGrid = function () {
            $gridHelper.getPage();
        }
    }]);