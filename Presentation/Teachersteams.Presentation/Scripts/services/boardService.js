angular.module('ttServices')
    .factory('$ttBoardService', [
        '$userHttp',
        '$q',
        '$vk',
        function ($userHttp, $q, $vk) {
            var getForStudentUrl = 'board/GetForStudent';
            var countForStudentUrl = 'board/CountForStudent';

            return { 
                getForStudent: function (filterType, paginationOptions) {
                    var data = null;
                    return $userHttp.get(getForStudentUrl, {
                        filterType: filterType,
                        pageNumber: paginationOptions.PageNumber,
                        pageSize: paginationOptions.PageSize,
                        sortingColumn: paginationOptions.SortingColumn,
                        sortingDirection: paginationOptions.SortingDirection
                    }).then(function (r) {
                        data = r.data;
                        var uids = _.map(data, function (item) {
                            return item.AssigneeTeacherUid;
                        });
                        uids = _.without(uids, null);
                        if (!uids.length) {
                            var deferred = $q.defer();
                            deferred.resolve({ response: [] });
                            return deferred.promise;
                        }
                        return $vk.call('users.get', {
                            uids: uids,
                            fields: "uid, first_name, last_name"
                        });
                    }).then(function(r) {
                        _.each(r.response, function(item) {
                            var originalItem = _.findWhere(data, { AssigneeTeacherUid: item.uid.toString() });
                            var name = item.last_name + ', ' + item.first_name;
                            originalItem["AssigneeName"] = name;
                        });
                        return data;
                    });
                },

                countForStudent: function (filterType) {
                    return $userHttp.get(countForStudentUrl, {
                        filterType: filterType
                    });
                }
            }
        }]);