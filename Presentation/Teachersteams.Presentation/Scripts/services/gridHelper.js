angular.module('ttServices')
    .factory('$gridHelper', ['SortingDirection', function (SortingDirection) {
        var refreshHeight = function (options) {
            var newHeight = Math.floor(options.data.length * 50 + 30);
            angular.element(document.getElementsByClassName('grid')[0]).css('height', (newHeight + 30) + 'px');
            angular.element(document.getElementsByClassName('ui-grid-viewport')[0]).css('height', newHeight + 'px');
        };

        var paginationOptions, fnItemsCount, fnItemsGet, fnParams, gridOptions;

        var getPageInternal = function () {
            fnItemsCount.apply(this, fnParams)
                .then(function (response) {
                    gridOptions.totalItems = response.data;
                });

            var getParams = fnParams.slice();
            getParams.push(paginationOptions);
            fnItemsGet.apply(this, getParams)
                .then(function (response) {
                    gridOptions.data = response;
                    refreshHeight(gridOptions);
                });
        }

        return {
            initialize: function(fnCount, fnGet, params, options) {
                paginationOptions = {
                    PageNumber: 1,
                    PageSize: 10,
                    SortingColumn: null,
                    SortingDirection: null
                };

                fnItemsCount = fnCount;
                fnItemsGet = fnGet;
                gridOptions = options;
                fnParams = params;
            },

            getPage: getPageInternal,

            sortChanged: function(grid, sortColumns) {
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
                getPageInternal();
            },

            paginationChanged: function (newPage, pageSize) {
                paginationOptions.PageNumber = newPage;
                paginationOptions.PageSize = pageSize;
                getPageInternal();
            }
        }
    }]);