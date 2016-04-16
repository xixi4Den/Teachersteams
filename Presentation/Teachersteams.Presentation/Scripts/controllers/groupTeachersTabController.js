var app = angular.module("ttControllers");
app.controller('ttGroupTeachersTabController', [
    '$scope',
    'ngDialog',
    'uiGridConstants',
    '$ttTeacherService',
    '$stateParams',
    '$gridHelper',
    function ($scope, ngDialog, uiGridConstants, $ttTeacherService, $stateParams, $gridHelper) {
        $scope.invite = function (e) {
            var dialog = ngDialog.open({ template: '/Teacher/Group/InviteTeacherDialog', controller: 'ttInviteTeacherDialogController' });
            dialog.closePromise.then(function (newGroup) {
                var result = newGroup.value;
                if ((typeof result !== "undefined") && result.hasOwnProperty('Id') && (typeof result.Id !== "undefined")) {
                    $scope.$broadcast('create_group', result);
                    $gridHelper.getPage();
                }
            });
        }

        $scope.test = function(grid, row) {
            console.log(row);
        }

        $scope.gridOptions = {
            rowHeight: 50,
            paginationPageSizes: [10],
            paginationPageSize: 10,
            useExternalPagination: true,
            useExternalSorting: true,
            enableHorizontalScrollbar: uiGridConstants.scrollbars.NEVER,
            enableVerticalScrollbar: uiGridConstants.scrollbars.NEVER,
            columnDefs: [
              { name: 'Photo', field: 'photo', enableSorting: false, width: 70, cellClass: 'ui-grid-hcenter', cellTemplate: 'photo-template' },
              { name: 'Uid', enableSorting: true, width: 100, cellClass: 'ui-grid-vcenter' },
              { name: 'FullName', enableSorting: false, width: 170, cellClass: 'ui-grid-vcenter' },
              { name: 'Actions', enableSorting: false, cellTemplate: 'actions-template' }
            ],
            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
                $scope.gridApi.core.on.sortChanged($scope, $gridHelper.sortChanged);
                $scope.gridApi.pagination.on.paginationChanged($scope, $gridHelper.paginationChanged);
            }
        };

        $gridHelper.initialize($ttTeacherService.count, $ttTeacherService.get, [$stateParams.groupId], $scope.gridOptions);
        $gridHelper.getPage();
    }]);