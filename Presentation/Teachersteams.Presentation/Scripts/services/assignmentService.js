angular.module('ttServices')
    .factory('$ttAssignmentService', [
        '$userHttp',
        'AppContext',
        function ($userHttp, AppContext) {
            var createUrl = 'assignment/Post';
            var getAllUrl = 'assignment/GetAll';
            var countUrl = 'assignment/Count';

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
                            switch(val.Status) {
                                case 1:
                                    val.StatusName = "Draft";
                                    break;
                                case 2:
                                    val.StatusName = "Active";
                                    break;
                                case 3:
                                    val.StatusName = "Expired";
                                    break;
                            }
                            val.FileUrl = AppContext.apiUrl + 'Assignment/Download?fileType=1&file=' + val.File;
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