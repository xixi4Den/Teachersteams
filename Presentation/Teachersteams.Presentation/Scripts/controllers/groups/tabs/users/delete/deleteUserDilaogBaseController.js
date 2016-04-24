var app = angular.module("ttControllers");
app.controller('ttDeleteUserDialogBaseController', [
    '$scope',
    'ngToast',
    function ($scope, ngToast) {
        var formParams = function() {
            var result = {}
            result.groupId = $scope.ngDialogData.groupId;
            result.uid = $scope.ngDialogData.user.Uid;
            return result;
        }

        $scope.no = function (e) {
            $scope.closeThisDialog(false);
        }

        $scope.yes = function () {
            var params = formParams();
            $scope.deleteFn(params)
            .then(function (response) {
                ngToast.success("User has been deleted");
                $scope.closeThisDialog(true);
            });
        }
    }]);