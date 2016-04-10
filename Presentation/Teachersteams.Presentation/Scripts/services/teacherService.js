angular.module('ttServices')
    .factory('$ttTeacherService', ['$userHttp', function ($userHttp) {
        var inviteUrl = 'teacher/Post';

        return {
            invite: function (viewModel) {
                return $userHttp.post(inviteUrl, viewModel);
            },

            //get: function (filterType, index, maxCount) {
            //    if (filterType < 0 || filterType > 2) {
            //        throw new Error("unsupported filter type");
            //    }
            //    if (index <= 0) {
            //        throw new Error("index for retrieving a portion of groups should be positive");
            //    }
            //    if (maxCount <= 0) {
            //        throw new Error("max count for retrieving a portion of groups should be positive");
            //    }

            //    return $userHttp.get(getUrl, {
            //        filterType: filterType,
            //        pageIndex: index,
            //        pageSize: maxCount
            //    });
            //}
        }
    }]);