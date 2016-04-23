var app = angular.module("ttControllers");
app.controller('ttUsersTabBaseController', [
    '$scope',
    'uiGridConstants',
    '$stateParams',
    '$gridHelper',
    function ($scope, uiGridConstants, $stateParams, $gridHelper) {
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

        $scope.initialize = function() {
            $gridHelper.initialize($scope.gridItemsCountFn, $scope.gridItemsGetFn, [$stateParams.groupId, $scope.userType], $scope.gridOptions);
            $gridHelper.getPage();
        }

        $scope.reload = function () {
            $gridHelper.getPage();
        }
    }]);