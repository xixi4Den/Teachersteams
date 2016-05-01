var app = angular.module("ttControllers");
app.controller('ttAssignmentsTabControllerForTeacher', [
    '$scope',
    'ngDialog',
    '$gridHelper',
    'uiGridConstants',
    '$ttAssignmentService',
    '$stateParams',
    function ($scope, ngDialog, $gridHelper, uiGridConstants, $ttAssignmentService, $stateParams) {
        $scope.create = function (e) {
            var dialog = ngDialog.open({ template: '/Teacher/Group/CreateAssignmentDialog', controller: 'ttCreateAssignmentsDialogController', closeByEscape: false, closeByNavigation: false, closeByDocument: false, showClose: false });
            dialog.closePromise.then(function (newAssignment) {
                var result = newAssignment.value;
                if ((typeof result !== "undefined") && result.hasOwnProperty('Id') && (typeof result.Id !== "undefined")) {
                    $scope.$broadcast('create_group', result);
                }
            });
        }

        $scope.gridItemsCountFn = $ttAssignmentService.count;
        $scope.gridItemsGetFn = $ttAssignmentService.get;

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

        $scope.gridOptions.columnDefs = [
              { name: "Title", field: 'Title', enableSorting: true, width: 150, cellClass: 'ui-grid-vcenter' },
              { name: "Expiration", field: 'ExpirationDate', enableSorting: true, width: 130, cellClass: 'ui-grid-vcenter', cellTemplate: 'date-template' },
              { name: "Status", field: 'StatusName', enableSorting: false, width: 80, cellClass: 'ui-grid-vcenter' },
              { name: "File", enableSorting: false, width: 90, cellClass: 'ui-grid-vcenter', cellTemplate: 'assignment-link-template'}
        ];

        $scope.initializeGrid = function () {
            $gridHelper.initialize($scope.gridItemsCountFn, $scope.gridItemsGetFn, [$stateParams.groupId], $scope.gridOptions);
            $gridHelper.getPage();
        }

        $scope.reloadGrid = function () {
            $gridHelper.getPage();
        }

        $scope.initializeGrid();
    }]);