var app = angular.module("ttControllers");
app.controller('ttInviteUserDialogBaseController', [
    '$scope',
    '$vk',
    'limitToFilter',
    'ngToast',
    '$stateParams',
    'UserSearchType',
    function ($scope, $vk, limitToFilter, ngToast, $stateParams, UserSearchType) {
        $scope.result = null;
        $scope.searchType = UserSearchType.Friends;

        function formInvitation() {
            var result = {};
            result.Uid = $scope.result.uid;
            result.GroupId = $stateParams.groupId;
            return result;
        }

        function findFriends(queryString) {
            return $vk.simpleCall('friends.get', {
                fields: "uid, first_name, last_name, photo_medium"
            }).then(function (r) {
                var result = _.filter(r.response, function (item) {
                    var format1 = item.last_name + " " + item.first_name;
                    var format2 = item.first_name + " " + item.last_name;
                    return format1.indexOf(queryString) >= 0
                        || format2.indexOf(queryString) >= 0;
                });
                return limitToFilter(result, 10);
            });
        }

        function findUsers(queryString) {
            return $vk.simpleCall('users.search', {
                q: queryString,
                fields: "uid, first_name, last_name, photo_medium"
            }).then(function (r) {
                var result = r.response.slice(1);
                return limitToFilter(result, 10);
            });
        }

        $scope.getUsers = function (queryString) {
            if ($scope.searchType === UserSearchType.Friends) {
                return findFriends(queryString);
            }
            if ($scope.searchType === UserSearchType.AllUsers) {
                return findUsers(queryString);
            }
            throw new Error("unsupported search type");
        }

        $scope.cancel = function (e) {
            $scope.closeThisDialog();
        }

        $scope.invite = function () {
            if ($scope.inviteUserForm.$valid) {
                var invitation = formInvitation();
                $scope.inviteInternal(invitation)
                    .then(function (response) {
                        ngToast.success("Invitation has been sent");
                        $scope.closeThisDialog(response.data);
                    });
            }
        }

        $scope.inviteInternal = function (invitation) { }
    }]);