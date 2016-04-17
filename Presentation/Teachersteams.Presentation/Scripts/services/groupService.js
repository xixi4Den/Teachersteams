angular.module('ttServices')
    .factory('$ttGroupService', ['$userHttp', function ($userHttp) {
        var getUrl = 'group/GetTitles';
        var getInfoUrl = 'group/Get/';
        var createUrl = 'group/Post';

        return {
            create: function (newGroup) {
                return $userHttp.post(createUrl, newGroup);
            },

            get: function (filterType, index, maxCount) {
                if (filterType < 0 || filterType > 3) {
                    throw new Error("unsupported group filter type");
                }
                if (index <= 0) {
                    throw new Error("index for retrieving a portion of groups should be positive");
                }
                if (maxCount <= 0) {
                    throw new Error("max count for retrieving a portion of groups should be positive");
                }

                return $userHttp.get(getUrl, {
                    filterType: filterType,
                    pageIndex: index,
                    pageSize: maxCount
                });
            },

            getInfo: function(groupId) {
                if (typeof groupId === "undefined") {
                    throw new Error("invalid argument");
                }

                return $userHttp.get(getInfoUrl + groupId);
            }
        }
    }]);