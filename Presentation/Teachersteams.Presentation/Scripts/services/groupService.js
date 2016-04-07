angular.module('ttServices')
    .factory('$ttGroupService', ['$userHttp', function ($userHttp) {
        var getUrl = 'group/GetForTeacher';
        var createUrl = 'group/Post';

        return {
            create: function (newGroup) {
                return $userHttp.post(createUrl, newGroup);
            },

            get: function (filterType, index, maxCount) {
                if (index <= 0) {
                    throw new Error("index for retrieving a portion of groups should be positive");
                }

                return $userHttp.get(getUrl, {
                    filterType: filterType,
                    pageIndex: index,
                    pageSize: maxCount
                });
            }
        }
    }]);