var app = angular.module("ttControllers");
app.controller('ttAssignmentsTabControllerForTeacher', ['$scope', 'ngDialog', function ($scope, ngDialog) {
    $scope.create = function (e) {
        var dialog = ngDialog.open({ template: '/Teacher/Group/CreateAssignmentDialog', controller: 'ttCreateAssignmentsDialogController', closeByEscape: false, closeByNavigation: false, closeByDocument: false, showClose: false });
        dialog.closePromise.then(function (newAssignment) {
            var result = newAssignment.value;
            if ((typeof result !== "undefined") && result.hasOwnProperty('Id') && (typeof result.Id !== "undefined")) {
                $scope.$broadcast('create_group', result);
            }
        });
    }
}]);