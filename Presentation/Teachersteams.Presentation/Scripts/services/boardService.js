angular.module('ttServices')
    .factory('$ttBoardService', [
        '$userHttp',
        function ($userHttp) {
            var getForStudentUrl = 'board/GetForStudent';
            var countForStudentUrl = 'board/CountForStudent';

            return { 
                getForStudent: function (filterType, paginationOptions) {
                    return $userHttp.get(getForStudentUrl, {
                        filterType: filterType,
                        pageNumber: paginationOptions.PageNumber,
                        pageSize: paginationOptions.PageSize,
                        sortingColumn: paginationOptions.SortingColumn,
                        sortingDirection: paginationOptions.SortingDirection
                    }).then(function (r) {
                        return r.data;
                    });
                },

                countForStudent: function (filterType) {
                    return $userHttp.get(countForStudentUrl, {
                        filterType: filterType
                    });
                }
            }
        }]);