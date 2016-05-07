var app = angular.module("ttControllers");
app.controller('ttViewAssignmentResultDialogController', [
    '$scope',
    '$ttAssignmentService',
    function ($scope, $ttAssignmentService) {
        $scope.max = 10;

        $scope.isChecked = function() {
            return $scope.data.Grade;
        }

        $ttAssignmentService.getResult($scope.ngDialogData.id).then(function (r) {
            $scope.data = r.data;
        });
       
        $scope.cancel = function (e) {
            $scope.closeThisDialog();
        }
    }]);