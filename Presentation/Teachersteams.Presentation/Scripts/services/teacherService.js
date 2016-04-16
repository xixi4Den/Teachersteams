﻿angular.module('ttServices')
    .factory('$ttTeacherService', ['$userHttp', '$vk', function ($userHttp, $vk) {
        var inviteUrl = 'Teacher/Post';
        var getUrl = 'Teacher/Get';
        var countUrl = 'Teacher/Count';

        return {
            invite: function (viewModel) {
                return $userHttp.post(inviteUrl, viewModel);
            },

            get: function (groupId, paginationOptions) {
                var data = null;
                return $userHttp.get(getUrl, {
                    groupId: groupId,
                    pageNumber: paginationOptions.PageNumber,
                    pageSize: paginationOptions.PageSize,
                    sortingColumn: paginationOptions.SortingColumn,
                    sortingDirection: paginationOptions.SortingDirection
                }).then(function (r) {
                    data = r.data;
                    var uids = _.map(data, function(item) {
                        return item.Uid;
                    });
                    return $vk.call('users.get', {
                        uids: uids,
                        fields: "uid, first_name, last_name, photo"
                    });
                }).then(function (r) {
                    _.each(r.response, function(item) {
                        var originalItem = _.findWhere(data, { Uid: item.uid.toString() });
                        var name = item.last_name + ', ' + item.first_name;
                        originalItem["FullName"] = name;
                        originalItem["Photo"] = item.photo;
                    });
                    return data;
                });
            },

            count: function (groupId) {
                return $userHttp.get(countUrl, {
                    groupId: groupId
                });
        }
        }
    }]);