var app = angular.module("ttControllers");
app.controller('ttGroupTeachersTabController', [
    '$scope',
    'ngDialog',
    'uiGridConstants',
    '$ttTeacherService',
    '$stateParams',
    'SortingDirection',
    function ($scope, ngDialog, uiGridConstants, $ttTeacherService, $stateParams, SortingDirection) {
        var paginationOptions = {
            PageNumber: 1,
            PageSize: 10,
            SortingColumn: null,
            SortingDirection: null
        };

        var refreshHeight = function () {
            var newHeight = Math.floor($scope.gridOptions.data.length * 50 + 30);
            angular.element(document.getElementsByClassName('grid')[0]).css('height', (newHeight + 30) + 'px');
            angular.element(document.getElementsByClassName('ui-grid-viewport')[0]).css('height', newHeight + 'px');
        };

        var getPage = function () {
            $ttTeacherService.count($stateParams.groupId)
                .then(function (response) {
                    $scope.gridOptions.totalItems = response.data;
                });

            $ttTeacherService.get($stateParams.groupId, paginationOptions)
                .then(function (response) {
                    $scope.gridOptions.data = response;
                    refreshHeight();
                });
        };

        $scope.invite = function (e) {
            var dialog = ngDialog.open({ template: '/Teacher/Group/InviteTeacherDialog', controller: 'ttInviteTeacherDialogController' });
            dialog.closePromise.then(function (newGroup) {
                var result = newGroup.value;
                if ((typeof result !== "undefined") && result.hasOwnProperty('Id') && (typeof result.Id !== "undefined")) {
                    $scope.$broadcast('create_group', result);
                    getPage();
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
                $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                    if (sortColumns.length === 0) {
                        paginationOptions.SortingColumn = null;
                        paginationOptions.SortingDirection = null;
                    } else {
                        if (sortColumns.length > 1) {
                            sortColumns[0].unsort();
                            return; // because unsort() triggers sortChanged again
                        }
                        paginationOptions.SortingColumn = sortColumns[0].field;
                        paginationOptions.SortingDirection = SortingDirection.getByCode(sortColumns[0].sort.direction);
                    }
                    getPage();
                });
                $scope.gridApi.pagination.on.paginationChanged($scope, function (newPage, pageSize) {
                    paginationOptions.PageNumber = newPage;
                    paginationOptions.PageSize = pageSize;
                    getPage();
                });
            }
        };

        getPage();
    }]);