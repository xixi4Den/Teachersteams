var app = angular.module("ttControllers");
app.controller('ttGradeAssignmentResultDialogController', [
    '$scope',
    '$ttAssignmentService',
    function ($scope, $ttAssignmentService) {
        $scope.max = 10;
        $scope.rate = 0;

        $scope.hoveringOver = function (value) {
            $scope.overStar = value;
            $scope.percent = 100 * (value / $scope.max);
        };

        $scope.hoveringOut = function (value) {
            $scope.overStar = null;
        };

        $scope.grade = function() {
            $ttAssignmentService.gradeResult($scope.ngDialogData.id, $scope.rate).then(function () {
                $scope.closeThisDialog($scope.rate);
            });
        }

        $scope.cancel = function () {
            $scope.closeThisDialog();
        }
    }]);