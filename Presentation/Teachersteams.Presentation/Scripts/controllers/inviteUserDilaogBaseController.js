var app = angular.module("ttControllers");
app.controller('ttInviteUserDialogBaseController', [
    '$scope',
    '$vk',
    'limitToFilter',
    'ngToast',
    '$stateParams',
    function ($scope, $vk, limitToFilter, ngToast, $stateParams) {
        $scope.result = null;

        function formInvitation() {
            var result = {};
            result.Uid = $scope.result.uid;
            result.GroupId = $stateParams.groupId;
            return result;
        }

        $scope.getUsers = function (queryString) {
            return $vk.simpleCall('users.search', {
                q: queryString,
                fields: "uid, first_name, last_name, photo_medium"
            }).then(function (r) {
                var result = r.response.slice(1);
                return limitToFilter(result, 10);
            });
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