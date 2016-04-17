var app = angular.module("ttControllers");
app.controller('ttGroupUsersTabBaseController', [
    '$scope',
    'ngDialog',
    'uiGridConstants',
    '$stateParams',
    '$gridHelper',
    function ($scope, ngDialog, uiGridConstants, $stateParams, $gridHelper) {
        $scope.inviteDilaogController = null;
        $scope.inviteDialogData = null;
        $scope.gridItemsCountFn = null;
        $scope.gridItemsGetFn = null;

        $scope.invite = function (e) {
            var dialog = ngDialog.open({ template: '/Teacher/Group/InviteUserDialog', controller: $scope.inviteDilaogController, data: $scope.inviteDialogData });
            dialog.closePromise.then(function (newUser) {
                var result = newUser.value;
                if ((typeof result !== "undefined") && result.hasOwnProperty('Id') && (typeof result.Id !== "undefined")) {
                    $gridHelper.getPage();
                }
            });
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
            }
        };

        $scope.initialize = function() {
            $gridHelper.initialize($scope.gridItemsCountFn, $scope.gridItemsGetFn, [$stateParams.groupId], $scope.gridOptions);
            $gridHelper.getPage();
        }
    }]);