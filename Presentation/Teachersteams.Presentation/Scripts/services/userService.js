angular.module('ttServices')
    .factory('$ttUserService', ['$userHttp', '$vk', function ($userHttp, $vk) {
        return function(urls) {
            return {
                invite: function(viewModel) {
                    return $userHttp.post(urls.inviteUrl, viewModel);
                },

                get: function(groupId, paginationOptions) {
                    var data = null;
                    return $userHttp.get(urls.getUrl, {
                        groupId: groupId,
                        pageNumber: paginationOptions.PageNumber,
                        pageSize: paginationOptions.PageSize,
                        sortingColumn: paginationOptions.SortingColumn,
                        sortingDirection: paginationOptions.SortingDirection
                    }).then(function(r) {
                        data = r.data;
                        var uids = _.map(data, function(item) {
                            return item.Uid;
                        });
                        return $vk.call('users.get', {
                            uids: uids,
                            fields: "uid, first_name, last_name, photo"
                        });
                    }).then(function(r) {
                        _.each(r.response, function(item) {
                            var originalItem = _.findWhere(data, { Uid: item.uid.toString() });
                            var name = item.last_name + ', ' + item.first_name;
                            originalItem["FullName"] = name;
                            originalItem["Photo"] = item.photo;
                        });
                        return data;
                    });
                },

                count: function(groupId) {
                    return $userHttp.get(urls.countUrl, {
                        groupId: groupId
                    });
                },

                requests: function() {
                    return $userHttp.get(urls.requestsUrl);
                },

                doesHaveRequest: function (groupId) {
                    return $userHttp.get(urls.doesHaveRequestUrl, {GroupId: groupId});
                },

                response: function(responseData) {
                    return $userHttp.post(urls.responseUrl, responseData);
                }
            }
        }
    }]);