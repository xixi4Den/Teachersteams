var app = angular.module("ttControllers");
app.controller('ttViewAssignmentResultDialogController', [
    '$scope',
    '$ttAssignmentService',
    function ($scope, $ttAssignmentService) {
        $scope.max = 10;

        $scope.data = {}

        $scope.isChecked = function() {
            return !(typeof $scope.data.Grade === 'undefined' || $scope.data.Grade === null);
        }

        $ttAssignmentService.getResult($scope.ngDialogData.id).then(function (r) {
            $scope.data = r.data;
        });
       
        $scope.cancel = function (e) {
            $scope.closeThisDialog();
        }
    }]);