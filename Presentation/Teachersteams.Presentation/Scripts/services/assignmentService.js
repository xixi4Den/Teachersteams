angular.module('ttServices')
    .factory('$ttAssignmentService', [
        '$userHttp',
        'AppContext',
        'AssignmentStatus',
        function ($userHttp, AppContext, AssignmentStatus) {
            var createUrl = 'assignment/Post';
            var getAllUrl = 'assignment/GetAll';
            var countUrl = 'assignment/Count';
            var downloadFileUrl = 'Assignment/Download?fileType=1&file=';

            return {
                create: function (newAssignment) {
                    return $userHttp.post(createUrl, newAssignment);
                },

                get: function (groupId, paginationOptions) {
                    return $userHttp.get(getAllUrl, {
                        groupId: groupId,
                        pageNumber: paginationOptions.PageNumber,
                        pageSize: paginationOptions.PageSize,
                        sortingColumn: paginationOptions.SortingColumn,
                        sortingDirection: paginationOptions.SortingDirection
                    }).then(function (r) {
                        _.map(r.data, function (val, key) {
                            val.StatusName = AssignmentStatus.localizedName[val.Status]();
                            val.FileUrl = '' + AppContext.apiUrl + downloadFileUrl + val.File;
                            return val;
                        });
                        return r.data;
                    });
                },

                count: function (groupId) {
                    return $userHttp.get(countUrl, {
                        groupId: groupId
                    });
                },
            }
    }]);